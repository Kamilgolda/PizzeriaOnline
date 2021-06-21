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

        public IActionResult Continuation()
        {
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
        public async Task<IActionResult> Details(int? id)
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

        //// GET: Order/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Order/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,LastName,Address,PhoneNumber,HasDelivery,Status")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        //// GET: Order/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(int id)
        //{
        //    return _context.Orders.Any(e => e.Id == id);
        //}
    }
}
