using MediatR;
using TicketSystem.Domain.Aggregates.Ticket;

namespace TicketSystem.Application.Queries.Tickets.GetTicketByNumber
{
    /// <summary>
    /// 獲取指定票券號碼的票券查詢
    /// </summary>
    public class GetTicketByNumberQuery : IRequest<Ticket>
    {
        /// <summary>
        /// 票券號碼
        /// </summary>
        public string TicketNumber { get; set; }
    }
} 