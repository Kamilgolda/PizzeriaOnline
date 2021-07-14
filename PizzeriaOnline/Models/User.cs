using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{   /*! Klasa Użytkownika*/
    public class User : IdentityUser
    {

        public string FirstName { get; set; } /*! Imię */
        public string LastName { get; set; } /*! Nazwisko */
        public string Address { get; set; } /*! Adres */

    }
}
