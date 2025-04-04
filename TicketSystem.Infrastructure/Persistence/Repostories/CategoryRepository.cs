using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence.Repostories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByTicketAsync(Guid ticketId)
        {
            return await _dbSet
                .Include(c => c.TicketCategories)
                .Where(c => c.TicketCategories.Any(tc => tc.TicketId == ticketId))
                .ToListAsync();
        }
    }
} 