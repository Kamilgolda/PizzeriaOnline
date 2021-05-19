using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzeriaOnline.Services
{
    public interface IProductsService
    {
        void Create(Product product);
        Task<bool> Delete(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int? id);
    }
}