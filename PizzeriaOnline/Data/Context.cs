using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaOnline.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PizzeriaOnline.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace PizzeriaOnline.Data
{   
    /*! Kontekst bazy danych */
    public class Context : IdentityDbContext<User>
    {
        /*! Konstruktor */
        public Context(DbContextOptions<Context> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }    /*! tabela Produktów */
        public DbSet<Component> Components { get; set; }    /*! tabela Składników */
        public DbSet<PricesForSizesProduct> PricesForSizesProducts { get; set; }  /*! tabela rozmiarów i cen dla produktów  */
        public DbSet<ComponentsProduct> ComponentsProducts { get; set; } /*! tabela skladnikow dla produktu */
        public DbSet<Order> Orders { get; set; } /*! tabela zamowień */
        public DbSet<ProductInOrder> ProductsInOrders { get; set; } /*! tabela produktów w zamówieniach */
        // public DbSet<User> UsersDbSet { get; set; } /*! Konstruktor */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
               new Product()
               {
                   Id = 1,
                   Title = "MARGHERITA",
                   Availability = true,
                   ImageName = "margherita.jpg",
                   ImageMimeType = "image/jpeg"
               },
               new Product()
               {
                   Id = 2,
                   Title = "VESUVIO",
                   Availability = true,
                   ImageName = "vesuvio.jpg",
                   ImageMimeType = "image/jpeg"
               },
               new Product()
               {
                   Id = 3,
                   Title = "SALAMI",
                   Availability = true,
                   ImageName = "salami.jpg",
                   ImageMimeType = "image/jpeg"
               },
               new Product()
               {
                   Id = 4,
                   Title = "QUATTRO FROMAGGI",
                   Availability = true,
                   ImageName = "formaggi.jpg",
                   ImageMimeType = "image/jpeg"
               },
               new Product()
               {
                   Id = 5,
                   Title = "LA NONNA",
                   Availability = true,
                   ImageName = "la_nonna.jpg",
                   ImageMimeType = "image/jpeg"
               },
               new Product()
               {
                   Id = 6,
                   Title = "PALERMO",
                   Availability = true,
                   ImageName = "palermo.jpg",
                   ImageMimeType = "image/jpeg"
               },
               new Product()
               {
                   Id = 7,
                   Title = "ITALIANA",
                   Availability = true,
                   ImageName = "italiana.jpg",
                   ImageMimeType = "image/jpeg"
               }

               );

            modelBuilder.Entity<PricesForSizesProduct>().HasData(
                new PricesForSizesProduct() { Id = 1, ProductId = 1, Size=ProductSize.small, Price= 13.5 },
                new PricesForSizesProduct() { Id = 2, ProductId = 1, Size=ProductSize.medium, Price= 18 },
                new PricesForSizesProduct() { Id = 3, ProductId = 1, Size=ProductSize.large, Price= 26 },
                new PricesForSizesProduct() { Id = 4, ProductId = 2, Size=ProductSize.small, Price= 15 },
                new PricesForSizesProduct() { Id = 5, ProductId = 2, Size=ProductSize.medium, Price= 24 },
                new PricesForSizesProduct() { Id = 6, ProductId = 2, Size=ProductSize.large, Price= 32 },
                new PricesForSizesProduct() { Id = 7, ProductId = 3, Size=ProductSize.small, Price= 16 },
                new PricesForSizesProduct() { Id = 8, ProductId = 3, Size=ProductSize.medium, Price= 24 },
                new PricesForSizesProduct() { Id = 9, ProductId = 3, Size=ProductSize.large, Price= 33 },
                new PricesForSizesProduct() { Id = 10, ProductId = 4, Size=ProductSize.small, Price= 17 },
                new PricesForSizesProduct() { Id = 11, ProductId = 4, Size=ProductSize.medium, Price= 25 },
                new PricesForSizesProduct() { Id = 12, ProductId = 4, Size=ProductSize.large, Price= 35 },
                new PricesForSizesProduct() { Id = 13, ProductId = 5, Size=ProductSize.small, Price= 15 },
                new PricesForSizesProduct() { Id = 14, ProductId = 5, Size=ProductSize.medium, Price= 23 },
                new PricesForSizesProduct() { Id = 15, ProductId = 5, Size=ProductSize.large, Price= 30 },
                new PricesForSizesProduct() { Id = 16, ProductId = 6, Size=ProductSize.small, Price= 18 },
                new PricesForSizesProduct() { Id = 17, ProductId = 6, Size=ProductSize.medium, Price= 26 },
                new PricesForSizesProduct() { Id = 18, ProductId = 6, Size=ProductSize.large, Price= 35 },
                new PricesForSizesProduct() { Id = 19, ProductId = 7, Size=ProductSize.small, Price= 19 },
                new PricesForSizesProduct() { Id = 20, ProductId = 7, Size=ProductSize.medium, Price= 26 },
                new PricesForSizesProduct() { Id = 21, ProductId = 7, Size=ProductSize.large, Price= 37 }
                );

            modelBuilder.Entity<ComponentsProduct>().HasData(
                new ComponentsProduct() { Id = 1, ProductId = 1, ComponentId = 1 },
                new ComponentsProduct() { Id = 2, ProductId = 1, ComponentId = 2 },
                new ComponentsProduct() { Id = 3, ProductId = 2, ComponentId = 1 },
                new ComponentsProduct() { Id = 4, ProductId = 2, ComponentId = 2 },
                new ComponentsProduct() { Id = 5, ProductId = 2, ComponentId = 3 },
                new ComponentsProduct() { Id = 6, ProductId = 2, ComponentId = 4 },
                new ComponentsProduct() { Id = 7, ProductId = 2, ComponentId = 5 },
                new ComponentsProduct() { Id = 8, ProductId = 3, ComponentId = 1 },
                new ComponentsProduct() { Id = 9, ProductId = 3, ComponentId = 2 },
                new ComponentsProduct() { Id = 10, ProductId = 3, ComponentId = 6 },
                new ComponentsProduct() { Id = 11, ProductId = 3, ComponentId = 7 },
                new ComponentsProduct() { Id = 12, ProductId = 3, ComponentId = 8 },
                new ComponentsProduct() { Id = 13, ProductId = 3, ComponentId = 9 },
                new ComponentsProduct() { Id = 14, ProductId = 4, ComponentId = 1 },
                new ComponentsProduct() { Id = 15, ProductId = 4, ComponentId = 2 },
                new ComponentsProduct() { Id = 16, ProductId = 4, ComponentId = 10 },
                new ComponentsProduct() { Id = 17, ProductId = 4, ComponentId = 11 },
                new ComponentsProduct() { Id = 18, ProductId = 4, ComponentId = 12 },
                new ComponentsProduct() { Id = 19, ProductId = 5, ComponentId = 1 },
                new ComponentsProduct() { Id = 20, ProductId = 5, ComponentId = 2 },
                new ComponentsProduct() { Id = 21, ProductId = 5, ComponentId = 13 },
                new ComponentsProduct() { Id = 22, ProductId = 5, ComponentId = 4 },
                new ComponentsProduct() { Id = 23, ProductId = 5, ComponentId = 5 },
                new ComponentsProduct() { Id = 24, ProductId = 6, ComponentId = 1 },
                new ComponentsProduct() { Id = 25, ProductId = 6, ComponentId = 2 },
                new ComponentsProduct() { Id = 26, ProductId = 6, ComponentId = 6 },
                new ComponentsProduct() { Id = 27, ProductId = 6, ComponentId = 11 },
                new ComponentsProduct() { Id = 28, ProductId = 6, ComponentId = 7 },
                new ComponentsProduct() { Id = 29, ProductId = 7, ComponentId = 14 },
                new ComponentsProduct() { Id = 30, ProductId = 7, ComponentId = 13 },
                new ComponentsProduct() { Id = 31, ProductId = 7, ComponentId = 15 },
                new ComponentsProduct() { Id = 32, ProductId = 7, ComponentId = 9 },
                new ComponentsProduct() { Id = 33, ProductId = 7, ComponentId = 16 },
                new ComponentsProduct() { Id = 34, ProductId = 7, ComponentId = 17 }
                );

            modelBuilder.Entity<Component>().HasData(
                new Component() { Id = 1, Name = "sos pomidorowy"},
                new Component() { Id = 2, Name = "ser mozzarella"},
                new Component() { Id = 3, Name = "szynka"},
                new Component() { Id = 4, Name = "pieczarki"},
                new Component() { Id = 5, Name = "kukurydza"},
                new Component() { Id = 6, Name = "salami"},
                new Component() { Id = 7, Name = "pomidorki koktajlowe"},
                new Component() { Id = 8, Name = "czarne oliwki"},
                new Component() { Id = 9, Name = "cebula czerwona"},
                new Component() { Id = 10, Name = "ser wędzony"},
                new Component() { Id = 11, Name = "ser pleśniowy"},
                new Component() { Id = 12, Name = "parmezan"},
                new Component() { Id = 13, Name = "kurczak"},
                new Component() { Id = 14, Name = "sos czosnkowy"},
                new Component() { Id = 15, Name = "pomidor suszony"},
                new Component() { Id = 16, Name = "szczypiorek"},
                new Component() { Id = 17, Name = "rukola"}
                );
        }                
    }

}
