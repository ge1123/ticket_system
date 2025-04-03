using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderAsync(Guid orderId)
        {
            return await _dbSet
                .Include(oi => oi.Ticket)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByTicketAsync(Guid ticketId)
        {
            return await _dbSet
                .Include(oi => oi.Order)
                .Where(oi => oi.TicketId == ticketId)
                .ToListAsync();
        }
    }
} 