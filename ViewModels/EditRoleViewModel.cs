using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{   
    /*! Klasa modelu zmiany roli użytkownika */
    public class EditRoleViewModel
    {
        public int Id { get; set; } /*! identyfikator użytkownika */
        [Required(ErrorMessage ="To pole jest wymagane")]
        public string RoleName{ get; set; } /*! nazwa roli */
        public List<string> Users { get; set; } /*! lista użytkowników */
    }
}
