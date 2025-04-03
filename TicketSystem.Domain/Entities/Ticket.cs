using System;
using System.Collections.Generic;
using TicketSystem.Domain.Enums;

namespace TicketSystem.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string TicketNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Venue { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        
        // 導航屬性
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<TicketCategory> Categories { get; set; }
    }
} 