using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Data;
using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Services
{
    public class ProductsService : IProductsService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ProductsService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var products = _context
                .Products
                .Include(o => o.Components).ThenInclude(c => c.Component)
                .Include(o => o.PricesForSizes)
                .ToList();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public ProductDto GetById(int id)
        {
            var product = _context
                .Products
                .Include(o => o.Components).ThenInclude(c => c.Component)
                .Include(o => o.PricesForSizes)
                .FirstOrDefault(o => o.Id == id);

            return product is null ? null : _mapper.Map<ProductDto>(product);
        }

        public int Create(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            _context.SaveChanges();

            return product.Id;
        }

        public bool Delete(int id)
        {
            var product = _context
               .Products
               .FirstOrDefault(o => o.Id == id);

            if (product is null) return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public bool Update(int id, UpdateProductDto dto)
        {
            var product = _context
               .Products
               .Include(o => o.Components).ThenInclude(c => c.Component)
               .Include(o => o.PricesForSizes)
               .FirstOrDefault(o => o.Id == id);

            if (product is null) return false;

            product.Title = dto.Title;
            product.Availability = dto.Availability;
            //product.PricesForSizes = _mapper.Map<PricesForSizesProductDto>(PricesForSizesProduct);
            //product.Components = dto.Components;
            _context.SaveChanges();
            return true;
        }

    }
}
