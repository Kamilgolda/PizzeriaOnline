using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models.Dto
{
    public class UpdateProductDto
    {
        
        public string Title { get; set; }

        
        public bool Availability { get; set; }
        public ICollection<PricesForSizesProductDto> PricesForSizes { get; set; }
        public ICollection<ComponentsProductDto> Components { get; set; }
    }
}
