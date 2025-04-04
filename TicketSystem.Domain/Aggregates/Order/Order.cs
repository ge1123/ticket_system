using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Enums;

namespace TicketSystem.Domain.Aggregates.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        // 導航屬性
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
} 