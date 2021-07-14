using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.ViewModels
{   /*! Klasa modelu produktu w zamówieniu */
    public class ProductInOrderViewModel
    {
        [Required()]
        public int ProductId { get; set; } /*! identyfikator produktu */
        [Required()]
        public int Size { get; set; } /*! rozmiar danego produktu */
        [Required()]
        public int Quantity { get; set; } /*! ilość danego produktu */
    }
}
