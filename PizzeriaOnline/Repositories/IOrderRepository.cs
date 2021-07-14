using PizzeriaOnline.Models;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Repositories
{   
    /*! Interfejs Repozytorium do zarządzania zamówieniami */
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> ActualOrders(); /*!< Pobranie listy aktualnych zamówień */
        Task ChangeStatus(int id, int status); /*!< Zmiana statusu zamówienia */
        Task<int> Create(OrderContinuationViewModel ordermodel); /*!< tworzenie zamówienia */
        Task Delete(int? id); /*!< usuwanie zamówienia */
        Task<IEnumerable<Order>> GetAll(); /*!< Pobranie listy wszystkich zamówień */
        Task<Order> GetById(int? id); /*!< Pobranie konkretnego zamówienia */
        Task Update(EditOrderViewModel ordermodel); /*!< aktualizacja zamówienia*/
        Task<IEnumerable<Order>> UserOrders(string UserID); /*!< Pobranie listy zamówień użytkownika */
    }
}
