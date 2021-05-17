using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{
    public class ComponentsProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int ComponentId { get; set; }
        public virtual Component Component { get; set; }

    }
}
