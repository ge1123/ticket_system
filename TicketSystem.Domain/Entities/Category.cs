using System;
using System.Collections.Generic;
using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Common;

namespace TicketSystem.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        // 導航屬性
        public virtual ICollection<TicketCategory> TicketCategories { get; set; }
    }
} 