using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using PizzeriaOnline.Repositories;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsPizzeria.FakeRepositories
{
    class FakeProductsRepository : IProductsRepository
    {
        public List<Product> products = new()
        {
            new Product { Id = 1, Availability = true, Components = new List<ComponentsProduct>(), ImageMimeType = "image/jpeg", ImageName = "italiana.jpg", PricesForSizes = new List<PricesForSizesProduct>(), Title = "Title1" },
            new Product { Id = 2, Availability = true, Components = new List<ComponentsProduct>(), ImageMimeType = "image/jpeg", ImageName = "italiana.jpg", PricesForSizes = new List<PricesForSizesProduct>(), Title = "Title2" },
            new Product { Id = 3, Availability = true, Components = new List<ComponentsProduct>(), ImageMimeType = "image/jpeg", ImageName = "italiana.jpg", PricesForSizes = new List<PricesForSizesProduct>(), Title = "Title3" }
        };

        public IQueryable<Component> ComponentsDropDownList()
        {
            throw new NotImplementedException();
        }

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
                    Size = ProductSize.small,
                    Price = productmodel.PriceForSmall
                },
                new PricesForSizesProduct()
                {
                    Size = ProductSize.medium,
                    Price = productmodel.PriceForMedium
                },
                new PricesForSizesProduct()
                {
                    Size = ProductSize.large,
                    Price = productmodel.PriceForLarge
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
                products.Add(product);
            }
            products.Add(product);
        }

        public async Task<bool> Delete(int id)
        {
            products.RemoveAt(id);
            return true;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return products;
        }

        public async Task<Product> GetById(int? id)
        {
            return products.Where(p => p.Id == id).SingleOrDefault();
        }

        public Task Update(EditProductViewModel productmodel)
        {
            throw new NotImplementedException();
        }
    }
}
