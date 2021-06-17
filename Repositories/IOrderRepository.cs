using PizzeriaOnline.Models;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaOnline.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> ActualOrders();
        Task<int> Create(OrderContinuationViewModel ordermodel);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int? id);
    }
}
