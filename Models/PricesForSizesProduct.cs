using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{
    public class PricesForSizesProduct
    {
        public int Id { get; set; }

        [Display(Name = "Rozmiar:")]
        [Required(ErrorMessage = "Proszę podać rozmiar")]
        public ProductSize? Size { get; set; }

        [Display(Name = "Cena:")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Required(ErrorMessage = "Proszę podać cenę")]
        [Range(1, 60)]
        public double Price { get; set; }

        public int ProductId { get; set; }

    }
}
