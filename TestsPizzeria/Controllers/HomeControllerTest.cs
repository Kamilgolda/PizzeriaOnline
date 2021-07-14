using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PizzeriaOnline.Controllers;
using PizzeriaOnline.Models;
using PizzeriaOnline.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsPizzeria.FakeRepositories;

namespace TestsPizzeria.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public async Task MenuModelShouldContainAllProductsAsync()
        {
            IProductsRepository fakeProductsRepository = new FakeProductsRepository();
            Mock<IWebHostEnvironment> environment = new Mock<IWebHostEnvironment>();
            HomeController homeController = new HomeController(fakeProductsRepository, environment.Object);
            ViewResult viewResult = await homeController.Menu() as ViewResult;
            List<Product> products = viewResult.Model as List<Product>;
            Assert.AreEqual(products.Count, 3);
        }

        [TestMethod]
        public async Task GetImageShouldReturnNotFoundWhenNotExistProduct()
        {
            IProductsRepository fakeProductsRepository = new FakeProductsRepository();
            Mock<IWebHostEnvironment> environment = new Mock<IWebHostEnvironment>();
            HomeController homeController = new HomeController(fakeProductsRepository, environment.Object);
            var result = await homeController.GetImage(5);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
