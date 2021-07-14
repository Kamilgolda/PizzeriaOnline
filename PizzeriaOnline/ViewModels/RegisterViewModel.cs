using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{   
    /*! Klasa modelu rejestracji użytkownika */
    public class RegisterViewModel : LoginViewModel
    {
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Wprowadź imię.")]
        public string FirstName { get; set; } /*! imię */

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Wprowadź nazwisko.")]
        public string LastName { get; set; } /*! nazwisko */

        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "Wprowadz numer telefonu.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } /*! numer telefonu */

        [Required(ErrorMessage = "Wprowadz email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } /*! adres email */

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Wprowadź adres.")]
        public string Adress { get; set; } /*! adres */
    }
}
