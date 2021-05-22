using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Data;
using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using PizzeriaOnline.Services;
using PizzeriaOnline.ViewModels;

namespace PizzeriaOnline.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Context _context;
        private readonly IProductsService _productsService;
        private readonly IHostingEnvironment _environment;

        public ProductsController(Context context, IProductsService productsService, IHostingEnvironment environment)
        {
            _context = context;
            _productsService = productsService;
            _environment = environment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productsService.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetById(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        private void ComponentsDropDownList(int? selectedcomponent = null)
        {
            var components = _productsService.ComponentsDropDownList();
            ViewBag.ComponentId = new SelectList(components.AsNoTracking(), "Id", "Name", selectedcomponent);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ComponentsDropDownList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel productmodel)
        {
            if (ModelState.IsValid)
            {
                await _productsService.Create(productmodel);
                return RedirectToAction(nameof(Index));
            }
            return View(productmodel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            EditProductViewModel editProductModel = new EditProductViewModel()
            {
                Id = product.Id,
                Availability = product.Availability,
                ImageMimeType = product.ImageMimeType,
                ImageName = product.ImageName,
                PhotoAvatar = product.PhotoAvatar,
                PhotoFile = product.PhotoFile,
                Title = product.Title,
                PricesForSizes = product.PricesForSizes.ToList(),
                Components = product.Components.ToList()
            };
            ComponentsDropDownList();
            return View(editProductModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel productmodel)
        {
            if (ModelState.IsValid)
            {
                await _productsService.Update(productmodel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productmodel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //GET_IMAGE

        public async Task<IActionResult> GetImage(int id)
        {
            Product requested = await _productsService.GetById(id);
            if (requested != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\images\\";
                string fullPath = webRootpath + folderPath + requested.ImageName;
                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, requested.ImageMimeType);
                }
                else
                {
                    if (requested.PhotoFile.Length > 0)
                    {
                        return File(requested.PhotoFile, requested.ImageMimeType);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }


    }
}
