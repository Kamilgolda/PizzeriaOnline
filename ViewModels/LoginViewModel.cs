using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{
        public class LoginViewModel
        {
            [Display(Name = "Nazwa użytkownika")]
            [Required(ErrorMessage = "Wpisz nazwą użytkownika.")]
            public string UserName { get; set; }

            [Display(Name = "Hasło")]
            [Required(ErrorMessage = "Wpisz hasło.")]
            public string Password { get; set; }

            [Display(Name = "Pamiętaj mnie")]
            public bool RememberMe { get; set; }
        }

}
