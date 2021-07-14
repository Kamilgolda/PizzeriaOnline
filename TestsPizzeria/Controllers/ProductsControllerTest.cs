using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PizzeriaOnline.Controllers;
using PizzeriaOnline.Models;
using PizzeriaOnline.Repositories;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsPizzeria.FakeRepositories;

namespace TestsPizzeria.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public async Task ProductsModelShouldContainAllProductsAsync()
        {
            IProductsRepository fakeProductsRepository = new FakeProductsRepository();
            Mock<IWebHostEnvironment> environment = new Mock<IWebHostEnvironment>();
            ProductsController productsController = new ProductsController(fakeProductsRepository, environment.Object);
            ViewResult viewResult = await productsController.Products() as ViewResult;
            List<Product> products = viewResult.Model as List<Product>;
            Assert.AreEqual(3, products.Count);
        }

        [TestMethod]
        public async Task CreateModelShouldAddNewProduct()
        { CreateProductViewModel productModel = new() { Title = "NewTitle", Availability = false, Components = new List<int?>(), NewComponents = new List<string>(), PriceForLarge = 3, PriceForMedium = 2, PriceForSmall = 1 };
            IProductsRepository fakeProductsRepository = new FakeProductsRepository();
            Mock<IWebHostEnvironment> environment = new Mock<IWebHostEnvironment>();
            ProductsController productsController = new ProductsController(fakeProductsRepository, environment.Object);
            var result = await productsController.Create(productModel);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual((result as RedirectToActionResult).ActionName, "Products");
            Assert.AreEqual((await fakeProductsRepository.GetAll()).Count(), 4);
        }

        [TestMethod]
        public async Task DeleteModelShouldDeleteProduct()
        {
            IProductsRepository fakeProductsRepository = new FakeProductsRepository();
            Mock<IWebHostEnvironment> environment = new Mock<IWebHostEnvironment>();
            ProductsController productsController = new ProductsController(fakeProductsRepository, environment.Object);
            await productsController.DeleteConfirmed(1);
            Assert.AreEqual((await fakeProductsRepository.GetAll()).Count(), 2);
        }


    }
}
