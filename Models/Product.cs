using Microsoft.AspNetCore.Http;
using PizzeriaOnline.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public bool Availability { get; set; }
        
        //Obrazek + Controler(getimg) + Repo (po => id)
        public string ImageName { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] PhotoFile { get; set; }

        [Display(Name = "Obrazek:")]
        [NotMapped]
        public IFormFile PhotoAvatar { get; set; }

        public virtual ICollection<PricesForSizesProduct> PricesForSizes { get; set; }
        public virtual ICollection<ComponentsProduct> Components {get;set;}
}
}
