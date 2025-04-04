using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Aggregates.Order;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Enums;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence.Repostories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Order> GetByOrderNumberAsync(string orderNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId)
        {
            return await _dbSet
                .Where(o => o.CreatedBy == userId)
                .ToListAsync();
        }
    }
} 