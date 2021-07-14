using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Availability { get; set; }

        public List<PricesForSizesProductDto> PricesForSizes { get; set; }
        public List<ComponentsProductDto> Components {get;set;}
    }
}
