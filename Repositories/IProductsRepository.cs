using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using PizzeriaOnline.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Repositories
{
    public interface IProductsRepository
    {
        IQueryable<Component> ComponentsDropDownList();
        Task Create(CreateProductViewModel productmodel);
        Task<bool> Delete(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int? id);
        Task Update(EditProductViewModel productmodel);
    }
}