using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{   /*! Klasa składnika */
    public class Component
    {
        public int Id { get; set; } /*! identyfikator składnika */
        
        [StringLength(50)]
        public string Name { get; set; } /*! nazwa składnika*/
    }
}
