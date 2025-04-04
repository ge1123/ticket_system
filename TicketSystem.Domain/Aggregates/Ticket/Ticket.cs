using TicketSystem.Domain.Aggregates.Order;
using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Enums;

namespace TicketSystem.Domain.Aggregates.Ticket;

public class Ticket : AggregateRoot
{
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

    public static Ticket Create(
        string title, string description, decimal price, int quantity,
        DateTime startDate, DateTime endDate, string venue, string createdBy)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("標題必填");
        if (price <= 0) throw new ArgumentException("價格必須大於 0");
        if (quantity <= 0) throw new ArgumentException("數量必須大於 0");
        if (startDate >= endDate) throw new ArgumentException("開始日期必須早於結束日期");

        return new Ticket
        {
            Id = Guid.NewGuid(),
            TicketNumber = GenerateTicketNumber(),
            Title = title,
            Description = description,
            Price = price,
            Quantity = quantity,
            StartDate = startDate,
            EndDate = endDate,
            Venue = venue,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = createdBy,
            UpdatedAt = DateTime.UtcNow,
            Status = TicketStatus.Active
        };
    }

    public void Update(string title, string description, decimal price, int quantity, DateTime startDate,
        DateTime endDate, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("標題必填");
        if (price <= 0) throw new ArgumentException("價格必須大於 0");
        if (quantity <= 0) throw new ArgumentException("數量必須大於 0");
        if (startDate >= endDate) throw new ArgumentException("開始日期必須早於結束日期");

        Title = title;
        Description = description;
        Price = price;
        Quantity = quantity;
        StartDate = startDate;
        EndDate = endDate;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    private static string GenerateTicketNumber()
    {
        return $"TKT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8]}";
    }
}