using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{       
        /*! Klasa modelu logowania */
        public class LoginViewModel
        {
            [Display(Name = "Nazwa użytkownika")]
            [Required(ErrorMessage = "Wpisz nazwą użytkownika.")]
            public string UserName { get; set; } /*! nazwa użytkownika */

            [Display(Name = "Hasło")]
            [Required(ErrorMessage = "Wpisz hasło.")]
            public string Password { get; set; } /*! hasło użytkownika */

            [Display(Name = "Pamiętaj mnie")]
            public bool RememberMe { get; set; } /*! zapamiętanie logowania: true-tak, false-nie */
        }

}
