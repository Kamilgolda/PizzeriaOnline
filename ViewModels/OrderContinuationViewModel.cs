using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{
    public class OrderContinuationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Imie:")]
        [Required(ErrorMessage = "Proszę podać imię")]
        [StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "Nazwisko:")]
        [Required(ErrorMessage = "Proszę podać nazwisko")]
        [StringLength(20)]
        public string LastName { get; set; }

        [Display(Name = "Adres:")]
        [Required(ErrorMessage = "Proszę podać adres")]
        [StringLength(50)]
        public string Address { get; set; }

        [Phone]
        [Display(Name = "Numer kontaktowy:")]
        [Required(ErrorMessage = "Proszę podać numer kontaktowy")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dowóz (do 20km) tylko dla zamówień o łącznej kwocie powyżej 50zł")]
        [Required()]
        public bool HasDelivery { get; set; }

        [Display(Name = "Produkty:")]
        public List<ProductInOrderViewModel> Products { get; set; }
    }
}
