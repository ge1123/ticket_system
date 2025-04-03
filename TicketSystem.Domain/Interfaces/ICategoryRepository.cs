using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetCategoriesByTicketAsync(Guid ticketId);
    }
} 