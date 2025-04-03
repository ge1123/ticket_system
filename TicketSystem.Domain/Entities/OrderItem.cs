using System;

namespace TicketSystem.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        // 導航屬性
        public virtual Order Order { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
} 