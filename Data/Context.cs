using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaOnline.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PizzeriaOnline.Data
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<PricesForSizesProduct> PricesForSizesProducts { get; set; }
        public DbSet<ComponentsProduct> ComponentsProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductsInOrder> ProductsInOrders { get; set; }
        public DbSet<User> UsersDbSet { get; set; } //Nazwa - bo nie przysłaniam contextu 

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
               }   
               );

            modelBuilder.Entity<PricesForSizesProduct>().HasData(
                new PricesForSizesProduct() { Id = 1, ProductId = 1, Size=ProductSize.small, Price= 13.50 },
                new PricesForSizesProduct() { Id = 2, ProductId = 1, Size=ProductSize.medium, Price= 18 },
                new PricesForSizesProduct() { Id = 3, ProductId = 1, Size=ProductSize.large, Price= 28 }
                );

            modelBuilder.Entity<ComponentsProduct>().HasData(
                new ComponentsProduct() { Id = 1, ProductId = 1, ComponentId = 1 },
                new ComponentsProduct() { Id = 2, ProductId = 1, ComponentId = 2 }
                );

            modelBuilder.Entity<Component>().HasData(
                new Component() { Id = 1, Name = "sos pomidorowy"},
                new Component() { Id = 2, Name = "ser mozzarella"}
                );

            modelBuilder.Entity<Order>().HasData(
                new Order() { Id = 1, Price= 555, Name= "Kamil", LastName= "Golda", Address="Lubenia", PhoneNumber="789456123", HasDelivery=true, Status=OrderStatus.Waiting }
                );

            modelBuilder.Entity<ProductsInOrder>().HasData(
                new ProductsInOrder() { Id = 1, OrderId = 1, ProductId = 1 }
                );

        }
                
    }

}
