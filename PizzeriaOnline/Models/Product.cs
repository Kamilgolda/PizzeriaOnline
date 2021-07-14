using Microsoft.AspNetCore.Http;
using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{   /*! Klasa produkt */
    public class Product
    {
        public int Id { get; set; } /*! identyfikator produktu */

        [Display(Name = "Nazwa:")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(50)]
        public string Title { get; set; } /*! nazwa produktu */

        [Display(Name = "Dostępność:")]
        public bool Availability { get; set; } /*! dostępność produktu */

        [Display(Name = "Obrazek:")]
        public string ImageName { get; set; } /*! nazwa obrazka  */
        public string ImageMimeType { get; set; } /*! rozszerzenie obrazka */
        public byte[] PhotoFile { get; set; } /*! plik obrazka */

        [Display(Name = "Obrazek:")]
        [NotMapped]
        public IFormFile PhotoAvatar { get; set; } /*! obrazek */

        public ICollection<PricesForSizesProduct> PricesForSizes { get; set; } /*! kolekcja cen i rozmiarów */
        public ICollection<ComponentsProduct> Components {get;set;} /*! kolekcja składników w produkcie */
}
}
