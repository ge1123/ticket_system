using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Enums;

namespace TicketSystem.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId);
    }
} 