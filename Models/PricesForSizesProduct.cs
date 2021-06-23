using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{   
    /*! Klasa cen i rozmiarów dla produktu */
    public class PricesForSizesProduct
    {
        public int Id { get; set; } /*! identyfikator */

        [Display(Name = "Rozmiar:")]
        [Required(ErrorMessage = "Proszę podać rozmiar")]
        public ProductSize? Size { get; set; } /*! rozmiar */

        [Display(Name = "Cena:")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Required(ErrorMessage = "Proszę podać cenę")]
        [Range(1, 60)]
        public double Price { get; set; } /*! cena */

        public int ProductId { get; set; } /*! identyfikator produktu */

    }
}
