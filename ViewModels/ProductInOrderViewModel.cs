using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{
    public class ProductInOrderViewModel
    {
        [Required()]
        public int ProductId { get; set; }
        [Required()]
        public int Size { get; set; }
        [Required()]
        public int Quantity { get; set; }
    }
}
