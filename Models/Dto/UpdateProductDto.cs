using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Models.Dto
{
    public class UpdateProductDto
    {
        [Display(Name = "Nazwa:")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "Dostępność:")]
        public bool Availability { get; set; }
        public ICollection<PricesForSizesProductDto> PricesForSizes { get; set; }
        public ICollection<ComponentsProductDto> Components { get; set; }
    }
}
