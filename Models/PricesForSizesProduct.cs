using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{
    public class PricesForSizesProduct
    {
        public int Id { get; set; }

        public ProductSize? Size { get; set; }

        public double Price { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
