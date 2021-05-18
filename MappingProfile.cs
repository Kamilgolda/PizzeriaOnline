using AutoMapper;
using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<PricesForSizesProduct, PricesForSizesProductDto>();
            CreateMap<ComponentsProduct, ComponentsProductDto>();
        }
    }
}
