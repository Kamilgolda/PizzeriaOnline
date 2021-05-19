using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Data;
using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Services
{
    public class ProductsService : IProductsService
    {
        private readonly Context _context;

        public ProductsService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context
                .Products
                .Include(o => o.Components).ThenInclude(c => c.Component)
                .Include(o => o.PricesForSizes)
                .ToListAsync();

            return products;
            
        }

        public async Task<Product> GetById(int? id)
        {
            var product = await _context
                .Products
                .Include(o => o.Components).ThenInclude(c => c.Component)
                .Include(o => o.PricesForSizes)
                .FirstOrDefaultAsync(o => o.Id == id);

            return product is null ? null : product;

        }

        public void Create(Product product)
        {
            if (product.PhotoAvatar != null && product.PhotoAvatar.Length > 0)
            {
                product.ImageMimeType = product.PhotoAvatar.ContentType;
                product.ImageName = Path.GetFileName(product.PhotoAvatar.FileName);
                using (var memoryStream = new MemoryStream())
                {
                    product.PhotoAvatar.CopyTo(memoryStream);
                    product.PhotoFile = memoryStream.ToArray();
                }
                _context.Add(product);
                _context.SaveChanges();
            }
            //await _context.Products.AddAsync(product);
            //await _context.SaveChangesAsync();

            //return product.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _context
               .Products
               .FirstOrDefaultAsync(o => o.Id == id);

            if (product is null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        //public bool Update(int id, UpdateProductDto dto)
        //{
        //    var product = _context
        //       .Products
        //       .Include(o => o.Components).ThenInclude(c => c.Component)
        //       .Include(o => o.PricesForSizes)
        //       .FirstOrDefault(o => o.Id == id);

        //    if (product is null) return false;

        //    product.Title = dto.Title;
        //    product.Availability = dto.Availability;
        //    //product.PricesForSizes = _mapper.Map<PricesForSizesProductDto>(PricesForSizesProduct);
        //    //product.Components = dto.Components;
        //    _context.SaveChanges();
        //    return true;
        //}

    }
}
