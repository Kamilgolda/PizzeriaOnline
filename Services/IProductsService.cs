using PizzeriaOnline.Models.Dto;
using System.Collections.Generic;

namespace PizzeriaOnline.Services
{
    public interface IProductsService
    {
        int Create(CreateProductDto dto);
        bool Delete(int id);
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
        bool Update(int id, UpdateProductDto dto);
    }
}