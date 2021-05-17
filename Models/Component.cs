using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[InverseProperty("Component")]
        //public virtual ICollection<ComponentsProduct> Products { get; set; }
    }
}
