using PizzeriaOnline.Enums;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{
    public class EditOrderViewModel
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        [Display(Name = "Kwota zamówienia:")]
        [Range(1, 5000)]
        public double Price { get; set; }

        [Display(Name = "Imie:")]
        [Required(ErrorMessage = "Proszę podać imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko:")]
        [Required(ErrorMessage = "Proszę podać nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Adres:")]
        [Required(ErrorMessage = "Proszę podać adres")]
        public string Address { get; set; }

        [Phone]
        [Display(Name = "Numer kontaktowy:")]
        [Required(ErrorMessage = "Proszę podać numer kontaktowy")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dowóz:")]
        public bool HasDelivery { get; set; }

        [Display(Name = "Status zamówienia:")]
        public OrderStatus Status { get; set; }

        public List<ProductInOrder> Products { get; set; }
    }
}
