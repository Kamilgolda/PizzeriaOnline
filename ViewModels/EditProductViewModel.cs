using Microsoft.AspNetCore.Http;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa:")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Dostępność:")]
        public bool Availability { get; set; }

        public string ImageName { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] PhotoFile { get; set; }

        [Display(Name = "Obrazek:")]
        public IFormFile PhotoAvatar { get; set; }

        public List<PricesForSizesProduct> PricesForSizes { get;set;}
        [Display(Name = "Składniki:")]
        public List<ComponentsProduct> Components { get; set; }

        [Display(Name = "Wybierz dodatkowe składniki:")]
        public List<int?> AddComponents { get; set; }

        [Display(Name = "Dodaj składniki:")]
        public List<string> NewComponents { get; set; }
    }
}
