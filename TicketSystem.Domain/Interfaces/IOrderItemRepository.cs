using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderAsync(Guid orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByTicketAsync(Guid ticketId);
    }
} 