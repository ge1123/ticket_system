using MediatR;
using TicketSystem.Domain.Aggregates.Ticket;

namespace TicketSystem.Application.Queries.Tickets.GetTicketById
{
    /// <summary>
    /// 獲取指定 ID 的票券查詢
    /// </summary>
    public class GetTicketByIdQuery : IRequest<Ticket>
    {
        /// <summary>
        /// 票券 ID
        /// </summary>
        public Guid Id { get; set; }
    }
} 