using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models.Dto
{
    public class PricesForSizesProductDto
    {
        //public int Id { get; set; }

        
        public ProductSize? Size { get; set; }

        
        public double Price { get; set; }
    }
}
