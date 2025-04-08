using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Queries.Tickets.GetTicketByNumber
{
    /// <summary>
    /// 獲取指定票券號碼的票券查詢處理程序
    /// </summary>
    public class GetTicketByNumberQueryHandler : IRequestHandler<GetTicketByNumberQuery, Ticket>
    {
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// 初始化查詢處理程序
        /// </summary>
        /// <param name="ticketRepository">票券倉儲</param>
        public GetTicketByNumberQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        /// <summary>
        /// 處理查詢
        /// </summary>
        /// <param name="request">查詢請求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>票券</returns>
        public async Task<Ticket> Handle(GetTicketByNumberQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByTicketNumberAsync(request.TicketNumber);
            if (ticket == null)
            {
                throw new KeyNotFoundException($"找不到票券號碼為 {request.TicketNumber} 的票券");
            }

            return ticket;
        }
    }
} 