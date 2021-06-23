using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Data;
using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Repositories
{
    /*! Repozytorium do zarządzania produktami */
    public class ProductsRepository : IProductsRepository
    {
        private readonly Context _context; /*!< Kontekst bazy danych */

        /**
        * Konstruktor.
        * @param context Kontekst bazy danych
        */
        public ProductsRepository(Context context)
        {
            _context = context;
        }

        /**
        * Task zwracający listę produktów.
        * @return Lista produktów.
        */
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context
                .Products
                .Include(o => o.Components).ThenInclude(c => c.Component)
                .Include(o => o.PricesForSizes)
                .ToListAsync();

            return products;
            
        }

        /**
        * Task zwracający produkt o podanym identyfikatorze
        * @param id identyfikator produktu
        * @return Lista produktów.
        */
        public async Task<Product> GetById(int? id)
        {
            var product = await _context
                .Products
                .Include(o => o.Components).ThenInclude(c => c.Component)
                .Include(o => o.PricesForSizes)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return product is null ? null : product;

        }

        /**
        * Task tworzący nowy produkt
        * @param productmodel Model produktu
        */
        public async Task Create(CreateProductViewModel productmodel)
        {
            List<ComponentsProduct> componentsProductList = new();
            foreach (int? componentId in productmodel.Components)
            {
                if (componentId != null) componentsProductList.Add(
                     new ComponentsProduct()
                     {
                         ComponentId = (int)componentId
                     });
            }

            foreach (string newcomponentname in productmodel.NewComponents)
            {
                if (newcomponentname != null) componentsProductList.Add(
                    new ComponentsProduct()
                    {
                        Component = new Component()
                        {
                            Name = newcomponentname.ToLower()
                        }
                    });
            }

            List<PricesForSizesProduct> pricesForSizesList = new()
                {
                    new PricesForSizesProduct()
                    {
                        Size=ProductSize.small,
                        Price=productmodel.PriceForSmall
                    },
                    new PricesForSizesProduct()
                    {
                        Size=ProductSize.medium,
                        Price=productmodel.PriceForMedium
                    },
                    new PricesForSizesProduct()
                    {
                        Size=ProductSize.large,
                        Price=productmodel.PriceForLarge
                    }
                };

            Product product = new()
            {
                Title = productmodel.Title,
                Availability = productmodel.Availability,
                PhotoAvatar = productmodel.PhotoAvatar,
                PricesForSizes = pricesForSizesList,
                Components = componentsProductList
            };

            if (product.PhotoAvatar != null && product.PhotoAvatar.Length > 0)
            {
                product.ImageMimeType = product.PhotoAvatar.ContentType;
                product.ImageName = Path.GetFileName(product.PhotoAvatar.FileName);
                using (var memoryStream = new MemoryStream())
                {
                    product.PhotoAvatar.CopyTo(memoryStream);
                    product.PhotoFile = memoryStream.ToArray();
                }
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
        }

         /**
        * Pobranie listy składników.
        * @return wszystkie składniki
        */
        public IQueryable<Component> ComponentsDropDownList()
        {
            var componentsQuery = from b in _context.Components
                                orderby b.Name
                                select b;
            return componentsQuery;
        }

        /**
        * Task usuwający produkt
        * @param id identyfikator produktu
        * @return true gdy usunięto produkt
        * @return false gdy nie usunięto produktu
        */
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

        /**
        * Task aktualizujący produkt
        * @param productmodel Model produktu
        */    
        public async Task Update(EditProductViewModel productmodel)
        {
            foreach (var componentProduct in productmodel.Components)
            {
                if (componentProduct.ComponentId == 0)
                {
                    ComponentsProduct cp = await _context.ComponentsProducts.FirstOrDefaultAsync(o => o.Id == componentProduct.Id);
                    _context.ComponentsProducts.Remove(cp);

                }
            }

            List<ComponentsProduct> componentsProductList = await _context.ComponentsProducts.Include(o => o.Component).Where(o => o.ProductId == productmodel.Id).ToListAsync();

            foreach (int? componentId in productmodel.AddComponents)
            {
                if (componentId != null) {
                    bool flag = false;
                    foreach (var c in componentsProductList)
                    {
                        if(c.ComponentId == (int)componentId)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if( flag == false)
                    {
                        componentsProductList.Add(new ComponentsProduct()
                        {
                            ComponentId = (int)componentId
                        });
                    }
                } 
            }

            var componentsincontext = ComponentsDropDownList();
            foreach (string componentProduct in productmodel.NewComponents)
            {
                if (componentProduct != null) {
                    bool flag = false;
                        foreach (var c in componentsincontext)
                        {
                            if (componentProduct == c.Name) 
                            {
                            flag = true;
                            break;
                            }
                        }
                    if (flag == false)
                    {
                        bool flag2 = false;
                        foreach( var cmp in componentsProductList)
                        {
                            if(cmp.Component.Name == componentProduct.ToLower())
                            {
                                flag2 = true;
                                break;
                            }
                        }

                        if(flag2 == false)
                        {
                            componentsProductList.Add(new ComponentsProduct()
                            {
                                Component = new Component()
                                {
                                    Name = componentProduct.ToLower()
                                }
                            });
                        }
                    } 
                } 
            }
            
            componentsProductList = componentsProductList.Distinct().ToList();

            Product product = new()
            {
                Id = productmodel.Id,
                Availability = productmodel.Availability,
                ImageName = productmodel.ImageName,
                ImageMimeType=productmodel.ImageMimeType,
                PhotoFile=productmodel.PhotoFile,
                PricesForSizes = productmodel.PricesForSizes,
                Title = productmodel.Title,
                Components = componentsProductList
                 
            };



            if (productmodel.PhotoAvatar != null && productmodel.PhotoAvatar.Length > 0)
            {
                product.ImageMimeType = productmodel.PhotoAvatar.ContentType;
                product.ImageName = Path.GetFileName(productmodel.PhotoAvatar.FileName);
                using var memoryStream = new MemoryStream();
                productmodel.PhotoAvatar.CopyTo(memoryStream);
                product.PhotoFile = memoryStream.ToArray();
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

    }
}
