using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{   
    /*! Klasa modelu edycji zamówienia */
    public class EditOrderViewModel
    {
        public int Id { get; set; } /*! identyfikator zamówienia */

        public string UserID { get; set; } /*! identyfikator użytkownika */

        [Display(Name = "Kwota zamówienia:")]
        [Range(1, 5000)]
        public double Price { get; set; } /*! kwota zamówienia */

        [Display(Name = "Imie:")]
        [Required(ErrorMessage = "Proszę podać imię")]
        public string Name { get; set; } /*! imie zamawiającego */

        [Display(Name = "Nazwisko:")]
        [Required(ErrorMessage = "Proszę podać nazwisko")]
        public string LastName { get; set; } /*! nazwisko zamawiającego */

        [Display(Name = "Adres:")]
        [Required(ErrorMessage = "Proszę podać adres")]
        public string Address { get; set; } /*! adres zamawiającego */

        [Phone]
        [Display(Name = "Numer kontaktowy:")]
        [Required(ErrorMessage = "Proszę podać numer kontaktowy")]
        public string PhoneNumber { get; set; } /*! numer kontaktowy zamawiającego */

        [Display(Name = "Dowóz:")]
        public bool HasDelivery { get; set; } /*! dowóz: 0-bez dowozu, 1-z dowozem */

        [Display(Name = "Status zamówienia:")]
        public OrderStatus Status { get; set; } /*! status zamówienia */

        public List<ProductInOrder> Products { get; set; } /*! Lista produktów w zamówieniu */
    }
}
