using Microsoft.AspNetCore.Http;
using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{   /*! Klasa modelu dla tworzenia produktu */
    public class CreateProductViewModel
    {

        [Display(Name = "Nazwa:")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(50)]
        public string Title { get; set; } /*! nazwa produktu */

        [Display(Name = "Dostępność:")]
        public bool Availability { get; set; } /*! dostępność produktu */

        [Required(ErrorMessage = "Proszę dodać obrazek")]
        [Display(Name = "Obrazek:")]
        public IFormFile PhotoAvatar { get; set; } /*! obrazek */

        [Display(Name = "Cena dla małej pizzy:")]
        [Required(ErrorMessage = "Proszę podać cenę dla małej pizzy")]
        [Range(1, 60)]
        public double PriceForSmall { get; set; } /*! cena dla małego rozmiaru */

        [Display(Name = "Cena dla średniej pizzy:")]
        [Required(ErrorMessage = "Proszę podać cenę dla średniej pizzy")]
        [Range(1, 60)]
        public double PriceForMedium { get; set; } /*! cena dla średniego rozmiaru */

        [Display(Name = "Cena dla dużej pizzy:")]
        [Required(ErrorMessage = "Proszę podać cenę dla dużej pizzy")]
        [Range(1, 60)]
        public double PriceForLarge { get; set; } /*! cena dla dużego rozmiaru */

        [Display(Name = "Składniki:")]
        public List<int?> Components { get; set; } /*! Lista identyfikatorów składników */

        [Display(Name = "Dodaj składniki:")]
        public List<string> NewComponents { get; set; } /*! Lista nazw nieistniejących składników */
    }
}
