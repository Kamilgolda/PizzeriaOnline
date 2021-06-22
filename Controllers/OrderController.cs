using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Data;
using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using PizzeriaOnline.Repositories;
using PizzeriaOnline.ViewModels;

namespace PizzeriaOnline.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductsRepository _productsRepository;
        private UserManager<User> _userManager;

        public OrderController(IOrderRepository orderRepository, IProductsRepository productsRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;
            _userManager = userManager;
        }
        [Authorize(Roles = "Worker,Admin")]
        public async Task<IActionResult> Orders()
        {
            return View(await _orderRepository.GetAll());
        }

        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var orders = await _orderRepository.UserOrders(user.Id);
            return View(orders);
        }

        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> ChangeStatus(int orderid, int status)
        {
            if (orderid == 0 || orderid == null || status == null ) return NotFound();

            await _orderRepository.ChangeStatus(orderid, status);
            return RedirectToAction(nameof(ActualOrders));
        }

        public IActionResult Continuation()
        {
            var actualday = DateTime.Now.DayOfWeek;
            var actualhour = DateTime.Now.Hour;
            if ((actualday == DayOfWeek.Monday && actualhour > 22 || actualhour < 14)
                || (actualday == DayOfWeek.Tuesday && actualhour > 22 || actualhour < 14)
                || (actualday == DayOfWeek.Wednesday && actualhour > 22 || actualhour < 14)
                || (actualday == DayOfWeek.Thursday && actualhour > 22 || actualhour < 14)
                || (actualday == DayOfWeek.Friday && actualhour < 14)
                || (actualday == DayOfWeek.Saturday && actualhour < 14)
                || (actualday == DayOfWeek.Sunday && actualhour < 14))
            {
                ViewBag.close = true;
            }
            else
            {
                ViewBag.close = false;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Continuation(OrderContinuationViewModel ordermodel)
        {
            if (ModelState.IsValid)
            {
                int orderid = await _orderRepository.Create(ordermodel);
                return RedirectToAction("Final", new { orderid });
            }
            return View(ordermodel);
        }

        [Authorize(Roles = "Worker,Admin")]
        public async Task<IActionResult> ActualOrders()
        {
            return View(await _orderRepository.ActualOrders());
        }

        [Authorize]
        public async Task<IActionResult> DetailsUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Product> products = new List<Product>();
            var order = await _orderRepository.GetById(id);
            foreach(ProductInOrder product in order.Products)
            {
                products.Add(await _productsRepository.GetById(product.ProductId));
            }
            ViewBag.products = products;
            if (order == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (order.UserID == user.Id)
                return View(order);
            else return NotFound();
        }

        [Authorize(Roles ="Worker, Admin")]
        public async Task<IActionResult> DetailsWorker(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Product> products = new List<Product>();
            var order = await _orderRepository.GetById(id);
            foreach (ProductInOrder product in order.Products)
            {
                products.Add(await _productsRepository.GetById(product.ProductId));
            }
            ViewBag.products = products;
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        public async Task<IActionResult> Final(int orderid)
        {
            var actual_orders = await _orderRepository.ActualOrders();
            int count = 0;
            int orderinqueue = 0;
            foreach (var order in actual_orders)
            {
                count++;
                if (order.Id == orderid)
                {
                    orderinqueue = count;
                }
            }
            var actualdate = DateTime.Now;
            var ready_date = actualdate.AddMinutes(orderinqueue * 15);
            ViewBag.orderinqueue = orderinqueue;
            ViewBag.time = ready_date.ToString("HH:mm");
            return View();
        }

        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Product> products = new List<Product>();
            var order = await _orderRepository.GetById(id);

            EditOrderViewModel orderModel = new EditOrderViewModel()
            {
                Id = order.Id,
                Address = order.Address,
                HasDelivery = order.HasDelivery,
                LastName = order.LastName,
                Name = order.Name,
                PhoneNumber = order.PhoneNumber,
                Price = order.Price,
                Products = order.Products.ToList(),
                Status = order.Status,
                UserID = order.UserID
            };

            foreach (ProductInOrder product in order.Products)
            {
                products.Add(await _productsRepository.GetById(product.ProductId));
            }
            ViewBag.products = products;
            if (order == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserID,Price,Name,LastName,Address,PhoneNumber,HasDelivery,Status,Products")] EditOrderViewModel orderModel)
        {
            if (id != orderModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _orderRepository.Update(orderModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ActualOrders));
            }
            return View(orderModel);
        }

        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _orderRepository.Delete(id);

            return RedirectToAction(nameof(ActualOrders));
        }

        private bool OrderExists(int id)
        {
            return _orderRepository.GetById(id) != null;
        }


    }
}
