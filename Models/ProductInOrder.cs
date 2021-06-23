using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{   /*! Klasa produktu w zamówieniu */
    public class ProductInOrder
    {
        public int Id { get; set; } /*! identyfikator */

        [Required()]
        public int OrderId { get; set; } /*! identyfikator zamówienia */
        [Required()]
        public int ProductId { get; set; } /*! identyfikator produktu */
        [Required()]
        public ProductSize Size { get; set; } /*! wybrany rozmiar danego produktu */
        [Required()]
        public int Quantity { get; set; } /*! podana ilosc danego produktu */
    }
}
