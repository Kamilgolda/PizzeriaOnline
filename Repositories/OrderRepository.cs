using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Data;
using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly Context _context;
        private readonly IProductsRepository _productsRepository;

        public OrderRepository(Context context, IProductsRepository productsRepository)
        {
            _context = context;
            _productsRepository = productsRepository;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _context
                .Orders
                .Include(o => o.Products)
                .ToListAsync();

            return orders;

        }

        public async Task<Order> GetById(int? id)
        {
            var order = await _context
                .Orders
                .Include(o => o.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return order is null ? null : order;

        }

        public async Task<IEnumerable<Order>> ActualOrders()
        {
            var orders = await _context
                .Orders
                .Include(o => o.Products)
                .AsNoTracking()
                .Where(o => o.Status != OrderStatus.Closed & o.Status != OrderStatus.Complete)
                .ToListAsync();

            return orders;
        }

        public async Task<int> Create(OrderContinuationViewModel ordermodel)
        {
            List<ProductInOrder> productsInOrder = new List<ProductInOrder>();
            double price = 0;
            foreach (var product in ordermodel.Products)
            {
                if (product.ProductId != 0)
                {
                    ProductSize size;
                    if (product.Size == 1) size = ProductSize.small;
                    else if (product.Size == 2) size = ProductSize.medium;
                    else size = ProductSize.large;

                    Product productFromContext =await _productsRepository.GetById(product.ProductId); 
                    foreach (var priceForSize in productFromContext.PricesForSizes)
                    {
                        if (priceForSize.Size == size) price += priceForSize.Price * product.Quantity;
                    }

                    productsInOrder.Add(new ProductInOrder() { ProductId = product.ProductId, Quantity = product.Quantity, Size = size });
                }
            }
            Order order = new Order()
            {
                Name = ordermodel.Name,
                LastName = ordermodel.LastName,
                Address = ordermodel.Address,
                PhoneNumber = ordermodel.PhoneNumber,
                Price = price,
                Products = productsInOrder,
                Status = OrderStatus.Waiting
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}
