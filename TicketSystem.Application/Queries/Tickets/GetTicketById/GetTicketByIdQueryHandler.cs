using MediatR;
using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Queries.Tickets.GetTicketById
{
    /// <summary>
    /// 獲取指定 ID 的票券查詢處理程序
    /// </summary>
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, Ticket>
    {
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// 初始化查詢處理程序
        /// </summary>
        /// <param name="ticketRepository">票券倉儲</param>
        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// 處理查詢
        /// </summary>
        /// <param name="request">查詢請求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>票券</returns>
        public async Task<Ticket> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.Id);
            if (ticket == null)
            {
                throw new KeyNotFoundException($"找不到 ID 為 {request.Id} 的票券");
            }

            return ticket;
        }
    }
} 