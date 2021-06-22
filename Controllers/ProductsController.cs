using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class ProductsController : Controller
    {
        private readonly Context _context;
        private readonly IProductsRepository _productsRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(Context context, IProductsRepository productsRepository, IWebHostEnvironment environment)
        {
            _context = context;
            _productsRepository = productsRepository;
            _environment = environment;
        }

        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Products()
        {
            return View(await _productsRepository.GetAll());
        }

        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.GetById(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        private void ComponentsDropDownList(int? selectedcomponent = null)
        {
            var components = _productsRepository.ComponentsDropDownList();
            ViewBag.ComponentId = new SelectList(components.AsNoTracking(), "Id", "Name", selectedcomponent);
        }

        [Authorize(Roles = "Worker, Admin")]
        public IActionResult Create()
        {
            ComponentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Create(CreateProductViewModel productmodel)
        {
            if (ModelState.IsValid)
            {
                await _productsRepository.Create(productmodel);
                return RedirectToAction(nameof(Products));
            }
            return View(productmodel);
        }

        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.GetById(id);

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
                PhotoFile = product.PhotoFile,
                Title = product.Title,
                PricesForSizes = product.PricesForSizes.ToList(),
                Components = product.Components.ToList()
            };
            ComponentsDropDownList();
            return View(editProductModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Edit(EditProductViewModel productmodel)
        {
            if (ModelState.IsValid)
            {
                await _productsRepository.Update(productmodel);
                return RedirectToAction(nameof(Products));
            }
            return View(productmodel);
        }

        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.GetById(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var removed = await _productsRepository.Delete(id);
            return removed ? RedirectToAction(nameof(Products)) : NotFound();
        }


        //GET_IMAGE

        public async Task<IActionResult> GetImage(int id)
        {
            Product requested = await _productsRepository.GetById(id);
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
