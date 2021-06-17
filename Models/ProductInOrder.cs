using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{
    public class ProductInOrder
    {
        public int Id { get; set; }

        [Required()]
        public int OrderId { get; set; }
        [Required()]
        public int ProductId { get; set; }
        [Required()]
        public ProductSize Size { get; set; }
        [Required()]
        public int Quantity { get; set; }
    }
}
