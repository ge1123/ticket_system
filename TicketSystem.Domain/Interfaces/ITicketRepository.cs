using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<Ticket> GetByTicketNumberAsync(string ticketNumber);
        Task<IEnumerable<Ticket>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Ticket>> GetAvailableTicketsAsync();
        Task<IEnumerable<Ticket>> GetTicketsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
} 