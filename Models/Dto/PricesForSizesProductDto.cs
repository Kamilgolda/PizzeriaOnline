using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models.Dto
{
    public class PricesForSizesProductDto
    {
        public int Id { get; set; }

        [Display(Name = "Rozmiar:")]
        [Required(ErrorMessage = "Proszę podać rozmiar")]
        public ProductSize? Size { get; set; }

        [Display(Name = "Cena:")]
        [Required(ErrorMessage = "Proszę podać cenę")]
        [Range(1, 60)]
        public double Price { get; set; }
    }
}
