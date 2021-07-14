using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzeriaOnline.Controllers;
using PizzeriaOnline.Models;
using PizzeriaOnline.Repositories;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PizzeriaTests
{
    public class UnitTest1
    {
        private List<Product> GetTestProducts()
        {
            var products = new List<Product>();

            products.Add(new Product()
            {
                Id = 1,
                Title = "Produkt1",
                Availability = true
            });
            products.Add(new Product()
            {
                Id = 2,
                Title = "Produkt2",
                Availability = true
            });

            return products;
        }

        [Fact]
        public async Task Menu_ReturnsViewResult_WithListOfProducts()
        {
            var mockRepo1 = new Mock<IProductsRepository>();
            var mockRepo2 = new Mock<IWebHostEnvironment>();
            mockRepo1.Setup(repo => repo.GetAll())
                .ReturnsAsync(GetTestProducts());
            var controller = new HomeController(mockRepo1.Object, mockRepo2.Object);

            //Act
            var result = await controller.Menu();

            //Assert 
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task ProductCreatePost_RedirectToProducts_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo1 = new Mock<IProductsRepository>();
            var mockRepo2 = new Mock<IWebHostEnvironment>();
            mockRepo1.Setup(repo => repo.GetAll())
                .ReturnsAsync(GetTestProducts());
            var controller = new ProductsController(mockRepo1.Object, mockRepo2.Object);
            CreateProductViewModel newProduct = new CreateProductViewModel()
            {
                Title = "Produkt3",
                Availability = true
            };

            // Act
            var result = await controller.Create(newProduct);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Products", redirectToActionResult.ActionName);
        }
    }
}
