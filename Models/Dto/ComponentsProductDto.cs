using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models.Dto
{
    public class ComponentsProductDto
    {
        public int ComponentId { get; set; }
        
        public virtual Component Component { get; set; }
    }
}
