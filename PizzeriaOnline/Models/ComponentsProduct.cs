using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{   /*! Klasa składnika w produkcie */
    public class ComponentsProduct
    {
        public int Id { get; set; } /*! identyfikator */ 
        public int ProductId { get; set; } /*! identyfikator produktu */
        public int ComponentId { get; set; } /*! identyfikator składnika */
        public Component Component { get; set; } /*! obiekt składnika */

    }
}
