using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{   
    /*! Klasa modelu składania zamówienia */
    public class OrderContinuationViewModel
    {
        public int Id { get; set; } /*! identyfikator zamówienia */

        public string UserID { get; set; } /*! identyfikator użytkownika */

        [Display(Name = "Imie:")]
        [Required(ErrorMessage = "Proszę podać imię")]
        [StringLength(20)]
        public string Name { get; set; } /*! imie zamawiającego */

        [Display(Name = "Nazwisko:")]
        [Required(ErrorMessage = "Proszę podać nazwisko")]
        [StringLength(20)]
        public string LastName { get; set; } /*! nazwisko zamawiającego */

        [Display(Name = "Adres:")]
        [Required(ErrorMessage = "Proszę podać adres")]
        [StringLength(50)]
        public string Address { get; set; } /*! adres zamawiającego */

        [Phone]
        [Display(Name = "Numer kontaktowy:")]
        [Required(ErrorMessage = "Proszę podać numer kontaktowy")]
        [StringLength(12)]
        public string PhoneNumber { get; set; } /*! numer kontaktowy zamawiającego */

        [Display(Name = "Dowóz (do 20km) tylko dla zamówień o łącznej kwocie powyżej 50zł")]
        [Required()]
        public bool HasDelivery { get; set; } /*! dowóz: false-bez dowozu, true-z dowozem */

        [Display(Name = "Produkty:")]
        public List<ProductInOrderViewModel> Products { get; set; } /*! Lista produktów w zamówieniu */
    }
}
