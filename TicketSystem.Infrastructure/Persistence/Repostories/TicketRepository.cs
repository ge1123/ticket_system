using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Enums;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence.Repostories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Ticket> GetByTicketNumberAsync(string ticketNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
        }

        public async Task<IEnumerable<Ticket>> GetByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Include(t => t.Categories)
                .Where(t => t.Categories.Any(tc => tc.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAvailableTicketsAsync()
        {
            return await _dbSet
                .Where(t => t.Status == TicketStatus.Active && t.Quantity > 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(t => t.StartDate >= startDate && t.EndDate <= endDate)
                .ToListAsync();
        }
    }
} 