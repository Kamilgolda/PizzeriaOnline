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
    /*! Kontroler do zarządzania produktami */
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository; /*!< Repozytorium dla produktów */
        private readonly IWebHostEnvironment _environment; /*!< Informacje o środowisku uruchomieniowym */

        /**
        * Konstruktor.
        * @param productsRepository Repozytorium do zarządzania produktami
        * @param environment Informacje o środowisku uruchomieniowym
        */
        public ProductsController(IProductsRepository productsRepository, IWebHostEnvironment environment)
        {
            _productsRepository = productsRepository;
            _environment = environment;
        }

        /**
        * Task odpowiadający wyświetleniu listy produktów.
        * @return Widok Products.cshtml
        */
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> Products()
        {
            return View(await _productsRepository.GetAll());
        }

        /**
        * Task odpowiadający wyświetleniu szczegółów produktu
        * @param id Identyfikator produktu
        * @return Widok Details.cshtml
        */
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

        /**
        * Funkcja pobierająca komponenty i tworząca obiekt ViewBag.ComponentId z listą wyboru.
        * @param selectedcomponent id z listy wyboru wybranego składnika.
        */
        private void ComponentsDropDownList(int? selectedcomponent = null)
        {
            var components = _productsRepository.ComponentsDropDownList();
            ViewBag.ComponentId = new SelectList(components.AsNoTracking(), "Id", "Name", selectedcomponent);
        }

        /**
        * Task odpowiadający tworzeniu produktu
        * @return Widok Create.cshtml
        */
        [Authorize(Roles = "Worker, Admin")]
        public IActionResult Create()
        {
            ComponentsDropDownList();
            return View();
        }

        /**
        * Task odpowiadający tworzeniu produktu
        * @param productmodel ViewModel do tworzenia produktów
        * @return Widok Products.cshtml gdy productmodel jest poprawnie zwalidowany
        * @return Widok Create.cshtml gdy productmodel nie jest poprawnie zwalidowany
        */
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

        /**
        * Task odpowiadający edycji produktu
        * @param id identyfikator produktu
        * @return Widok Edit.cshtml gdy produkt o podanym id istneje.
        * @return Widok NotFound gdy produkt o podanym id nie istnieje.
        */
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
            EditProductViewModel editProductModel = new()
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

        /**
        * Task odpowiadający edycji produktu
        * @param productmodel ViewModel do edycji produktów
        * @return Widok Products.cshtml gdy productmodel jest poprawnie zwalidowany
        * @return Widok Edit.cshtml gdy productmodel nie jest poprawnie zwalidowany
        */
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

        /**
        * Task odpowiadający usuwaniu produktu
        * @param id identyfikator produktu
        * @return Widok Delete.cshtml gdy produkt o podanym id istneje.
        * @return Widok NotFound gdy produkt o podanym id nie istnieje.
        */
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

        /**
        * Task odpowiadający potwierdzeniu usuwania produktu
        * @param id identyfikator produktu
        * @return Widok Products.cshtml gdy produkt zostanie usunięty.
        * @return Widok NotFound gdy produkt o podanym id nie istnieje.
        */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Worker, Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var removed = await _productsRepository.Delete(id);
            return removed ? RedirectToAction(nameof(Products)) : NotFound();
        }


        //GET_IMAGE
        /**
        * Task odpowiadający pobraniu obrazka danego produktu
        * @param id identyfikator produktu
        * @return File jeśli obrazek dla danego produktu istnieje
        * @return Widok NotFound gdy obrazek dla produktu o podanym id nie istnieje.
        */
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
                    FileStream fileOnDisk = new(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new(fileOnDisk))
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
