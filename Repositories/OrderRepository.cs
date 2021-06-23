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
    /*! Repozytorium do zarządzania zamówieniami */
    public class OrderRepository:IOrderRepository
    {
        private readonly Context _context; /*!< Kontekst bazy danych */
        private readonly IProductsRepository _productsRepository; /*!< repozytorium produktów */

        /*
        * Konstruktor
        * @param context kontekst bazy danych
        * @param productsRepository repozytorium produktów
        */
        public OrderRepository(Context context, IProductsRepository productsRepository)
        {
            _context = context;
            _productsRepository = productsRepository;
        }

        /**
        * Task zwracający listę wszystkich zamówień
        * @return Lista zamówień
        */
        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _context
                .Orders
                .Include(o => o.Products)
                .ToListAsync();

            return orders;
        }

        /**
        * Task zwracający zamówienie o podanym identyfikatorze
        * @param id identyfikator zamówienia
        * @return zamówienie.
        */
        public async Task<Order> GetById(int? id)
        {
            var order = await _context
                .Orders
                .Include(o => o.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return order is null ? null : order;

        }
        
        /**
        * Task zwracający listę aktualnych zamówień
        * @return Lista aktualnych zamówień
        */
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

        /**
        * Task zwracający listę zamówień dla danego użytkownika
        * @param UserID identyfikator użytkownika
        * @return Lista aktualnych zamówień
        */
        public async Task<IEnumerable<Order>> UserOrders(string UserID)
        {
            var orders = await _context
                .Orders
                .Include(o => o.Products)
                .AsNoTracking()
                .Where(o => o.UserID==UserID)
                .ToListAsync();

            orders.Reverse();

            return orders;
        }

        /**
        * Task tworzący zamówienie
        * @param ordermodel model zamówienia
        * @return identyfukator zamówienia
        */
        public async Task<int> Create(OrderContinuationViewModel ordermodel)
        {
            List<ProductInOrder> productsInOrder = new();
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
            Order order = new()
            {
                UserID = ordermodel.UserID,
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

         /**
        * Task aktualizujący zamówienie
        * @param ordermodel model zamówienia
        */
        public async Task Update(EditOrderViewModel ordermodel)
        {
            Order order = new()
            {
                Id = ordermodel.Id,
                Address = ordermodel.Address,
                HasDelivery = ordermodel.HasDelivery,
                LastName = ordermodel.LastName,
                Name = ordermodel.Name,
                PhoneNumber = ordermodel.PhoneNumber,
                Price = ordermodel.Price,
                Products = ordermodel.Products,
                Status = ordermodel.Status,
                UserID = ordermodel.UserID
            };
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

         /**
        * Task usuwający zamówienie
        * @param id identyfikator zamówienia
        */
        public async Task Delete(int? id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

         /**
        * Task zmieniający status zamówienia
        * @param id identyfikator zamówienia
        * @param status nowy status zamówienia
        */
        public async Task ChangeStatus(int id, int status)
        {
            OrderStatus s = OrderStatus.Waiting;
            if (status == 0) s = OrderStatus.Waiting;
            if (status == 1) s = OrderStatus.Started;
            if (status == 2) s = OrderStatus.InDelivery;
            if (status == 3) s = OrderStatus.Complete;
            if (status == 4) s = OrderStatus.Closed;
            var order =await GetById(id);
            order.Status = s;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
