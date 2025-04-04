using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Aggregates.Ticket
{
    public class TicketCategory : Entity
    {
        public Guid TicketId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        // 導航屬性
        public virtual Ticket Ticket { get; set; }
        public virtual Category Category { get; set; }
    }
} 