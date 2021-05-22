using Microsoft.AspNetCore.Http;
using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{
    public class CreateProductViewModel
    {

        [Display(Name = "Nazwa:")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Dostępność:")]
        public bool Availability { get; set; }

        [Required(ErrorMessage = "Proszę dodać obrazek")]
        [Display(Name = "Obrazek:")]
        public IFormFile PhotoAvatar { get; set; }

        [Display(Name = "Cena dla małej pizzy:")]
        [Required(ErrorMessage = "Proszę podać cenę dla małej pizzy")]
        [Range(1, 60)]
        public double PriceForSmall { get; set; }

        [Display(Name = "Cena dla średniej pizzy:")]
        [Required(ErrorMessage = "Proszę podać cenę dla średniej pizzy")]
        [Range(1, 60)]
        public double PriceForMedium { get; set; }

        [Display(Name = "Cena dla dużej pizzy:")]
        [Required(ErrorMessage = "Proszę podać cenę dla dużej pizzy")]
        [Range(1, 60)]
        public double PriceForLarge { get; set; }

        [Display(Name = "Składniki:")]
        public List<int?> Components { get; set; }

        [Display(Name = "Dodaj składniki:")]
        public List<string> NewComponents { get; set; }
    }
}
