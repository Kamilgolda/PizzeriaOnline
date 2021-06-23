using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using PizzeriaOnline.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Repositories
{   
    /*! Interfejs Repozytorium do zarządzania produktami */
    public interface IProductsRepository
    {
        IQueryable<Component> ComponentsDropDownList(); /*!< Pobranie listy komponentów */

        Task Create(CreateProductViewModel productmodel); /*!< Tworzenie produktu */

        Task<bool> Delete(int id); /*!< Usuwanie produktu */

        Task<IEnumerable<Product>> GetAll(); /*!< Pobranie listy wszystkich produktów */

        Task<Product> GetById(int? id); /*!< Pobranie produktu o podanym identyfikatorze */

        Task Update(EditProductViewModel productmodel); /*!< Aktualizacja produktu */

    }
}