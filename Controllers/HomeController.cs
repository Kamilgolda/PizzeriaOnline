using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzeriaOnline.Data;
using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using PizzeriaOnline.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Controllers
{   /*! Główny kontroler */
    public class HomeController : Controller
    {
        private readonly IProductsRepository _productsRepository; /*! repozytorium produktów */
        private readonly IWebHostEnvironment _environment; /*!< Informacje o środowisku uruchomieniowym */

         /**
        * Konstruktor.
        * @param productsRepository Repozytorium do zarządzania produktami
        * @param environment Informacje o środowisku uruchomieniowym
        */
        public HomeController(IProductsRepository productsRepository, IWebHostEnvironment environment)
        {
            _productsRepository = productsRepository;
            _environment = environment;
        }
        
        /**
        * wyświetlenie strony głównej.
        * @return Index.cshtml
        */
        public IActionResult Index()
        {
            return View();
        }

        /**
        * Task odpowiadający wyświetleniu menu
        * @return Widok Menu.cshtml
        */
        public async Task<IActionResult> Menu()
        {
            var listaproduktow = await _productsRepository.GetAll();
            return View(listaproduktow);
        }

        /**
        * Task odpowiadający wyświetleniu panelu pracownika
        * @return Widok WorkerPanel.cshtml
        */
        [Authorize(Roles = "Worker, Admin")]
        public IActionResult WorkerPanel()
        {
            return View();
        }

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


        /**
        * wyświetlenie błędu
        * @return Widok Error.cshtml
        */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
