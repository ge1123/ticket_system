using System;

namespace TicketSystem.Domain.Entities
{
    public class TicketCategory
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