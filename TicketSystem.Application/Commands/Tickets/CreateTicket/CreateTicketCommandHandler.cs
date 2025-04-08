using MediatR;
using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Commands.Tickets.CreateTicket;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateTicketCommandHandler(ITicketRepository ticketRepository, ICategoryRepository categoryRepository)
    {
        _ticketRepository = ticketRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Ticket> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
    {
        // 驗證分類是否存在
        if (command.CategoryIds != null && command.CategoryIds.Any())
        {
            foreach (var categoryId in command.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    throw new ArgumentException($"找不到 ID 為 {categoryId} 的分類");
                }
            }
        }
        // 使用 Entity 內部的 Create 方法
        var ticket = Ticket.Create(
            command.Title,
            command.Description,
            command.Price,
            command.Quantity,
            command.StartDate,
            command.EndDate,
            command.Venue,
            command.CreatedBy
        );
        
        // 新增票券
        await _ticketRepository.AddAsync(ticket);
        
        // 設定分類
        if (command.CategoryIds != null && command.CategoryIds.Any())
        {
            var ticketCategories = command.CategoryIds.Select(categoryId =>
                new TicketCategory
                {
                    TicketId = ticket.Id,
                    CategoryId = categoryId
                });
        
            ticket.Categories = ticketCategories.ToList();
        }
        
        return ticket;
    }
}