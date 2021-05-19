﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzeriaOnline.Data;
using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using PizzeriaOnline.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsService _productsService;
        private readonly IHostingEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService, IHostingEnvironment environment)
        {
            _logger = logger;
            _productsService = productsService;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var listaproduktow = _productsService.GetAll();
            return View(listaproduktow);
        }

        public IActionResult Privacy()
        {
            return View();
        }

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}