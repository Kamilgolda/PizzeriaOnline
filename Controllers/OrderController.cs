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
    /*! Kontroler do zarządzania zamówieniami */
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository; /*!< Repozytorium dla zamówień */
        private readonly IProductsRepository _productsRepository; /*!< Repozytorium dla produktów */
        private readonly UserManager<User> _userManager; /*!< Manadżer użytkownika */

        /**
        * Konstruktor.
        * @param orderRepository Reposytorium do zarządzania zamówieniami
        * @param productsRepository Repozytorium do zarządzania produktami
        * @param userManager Manadżer użytkownika 
        */
        public OrderController(IOrderRepository orderRepository, IProductsRepository productsRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;
            _userManager = userManager;
        }

        /**
        * Task odpowiadający wyświetleniu listy wszystkich zamówień.
        * @return Widok Orders.cshtml
        */
        [Authorize(Roles = "Worker,Admin")]
        public async Task<IActionResult> Orders()
        {
            return View(await _orderRepository.GetAll());
        }

        /**
        * Task odpowiadający wyświetleniu listy wszystkich zamówień użytkownika.
        * @return Widok MyOrders.cshtml
        */
        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var orders = await _orderRepository.UserOrders(user.Id);
            return View(orders);
        }

        /**
        * Task odpowiadający zmianie statusu zamówienia
        * @param orderid identyfikator zamówienia
        * @param status nowy status dla zamówienia
        * @return Widok ActualOrders.cshtml
        */
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> ChangeStatus(int orderid, int status)
        {
            if (orderid == 0) return NotFound();

            await _orderRepository.ChangeStatus(orderid, status);
            return RedirectToAction(nameof(ActualOrders));
        }

        /**
        * Task odpowiadający składaniu zamówienia
        * @return Widok Continuation.cshtml
        */
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

        /**
        * Task odpowiadający składaniu zamówienia
        * @param ordermodel model zamówienia
        * @return Widok Final.cshtml gdy ordermodel jest poprawnie zwalidowany
        * @return Widok Continuation.cshtml gdy ordermodel nie jest poprawnie zwalidowany
        */
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

        /**
        * Task odpowiadający wyświetleniu listy aktualnych zamówień.
        * @return Widok ActualOrders.cshtml
        */
        [Authorize(Roles = "Worker,Admin")]
        public async Task<IActionResult> ActualOrders()
        {
            return View(await _orderRepository.ActualOrders());
        }

        /**
        * Task odpowiadający wyświetleniu szczegółów zamówienia zalogowanego użytkownika
        * @param id Identyfikator zamówienia
        * @return Widok DetailsUser.cshtml
        */
        [Authorize]
        public async Task<IActionResult> DetailsUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Product> products = new();
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

        /**
        * Task odpowiadający wyświetleniu szczegółów zamówienia
        * @param id Identyfikator zamówienia
        * @return Widok DetailsWorker.cshtml
        */
        [Authorize(Roles ="Worker, Admin")]
        public async Task<IActionResult> DetailsWorker(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Product> products = new();
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

        /**
        * Task odpowiadający potwierdzeniu złożenia zamówienia
        * @param orderid identyfikator zamówienia
        * @return Widok Final.cshtml
        */
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

        /**
        * Task odpowiadający edycji zamówienia
        * @param id identyfikator zamówienia
        * @return Widok Edit.cshtml gdy zamówienie o podanym id istneje.
        * @return Widok NotFound gdy zamówienie o podanym id nie istnieje.
        */
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Product> products = new();
            var order = await _orderRepository.GetById(id);

            EditOrderViewModel orderModel = new()
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

        /**
        * Task odpowiadający edycji zamówienia
        * @param id identyfikator zamówienia
        * @param ordermodel model edytowanego zamówienia
        * @return Widok ActualOrders.cshtml gdy ordermodel jest poprawnie zwalidowany
        * @return Widok Edit.cshtml gdy ordermodel nie jest poprawnie zwalidowany
        */
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

        /**
        * Task odpowiadający usuwaniu zamówienia
        * @param id identyfikator zamówienia
        * @return Widok ActualOrders.cshtml gdy produkt o podanym id istneje.
        * @return Widok NotFound gdy zamówienie o podanym id nie istnieje.
        */
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _orderRepository.Delete(id);

            return RedirectToAction(nameof(ActualOrders));
        }

        /**
        * Sprawdza czy zamówienie o podanym identyfikatorze istnieje
        * @param id identyfikator zamówienia
        * @return true - istnieje
        * @return false - nie istnieje
        */
        private bool OrderExists(int id)
        {
            return _orderRepository.GetById(id) != null;
        }


    }
}
