using Microsoft.AspNetCore.Http;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{   
    /*! Klasa modelu edycji produktu */
    public class EditProductViewModel
    {
        public int Id { get; set; } /*! identyfikator produktu */

        [Display(Name = "Nazwa:")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(50)]
        public string Title { get; set; } /*! nazwa produktu */

        [Display(Name = "Dostępność:")]
        public bool Availability { get; set; } /*! dostępność produktu */

        public string ImageName { get; set; } /*! nazwa obrazka  */
        public string ImageMimeType { get; set; } /*! rozszerzenie obrazka */
        public byte[] PhotoFile { get; set; } /*! plik obrazka */

        [Display(Name = "Obrazek:")]
        public IFormFile PhotoAvatar { get; set; } /*! obrazek */

        public List<PricesForSizesProduct> PricesForSizes { get;set;} /*! lista cen i rozmiarów */
        [Display(Name = "Składniki:")]
        public List<ComponentsProduct> Components { get; set; } /*! lista składników w produkcie */

        [Display(Name = "Wybierz dodatkowe składniki:")]
        public List<int?> AddComponents { get; set; } /*! lista identyfikatorów składników do dodania */

        [Display(Name = "Dodaj składniki:")]
        public List<string> NewComponents { get; set; } /*! lista nazw nieistniejących składników */
    }
}
