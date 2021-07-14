using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models.Dto
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public bool Availability { get; set; }
    }
}
