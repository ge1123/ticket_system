using FluentValidation;

namespace TicketSystem.Application.Commands.Tickets.CreateTicket;

public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("標題不能為空")
            .MaximumLength(100).WithMessage("標題長度不能超過 100 個字元");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("描述長度不能超過 500 個字元");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("價格必須大於 0");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("數量不能為負數");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("開始日期不能為空")
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("開始日期必須早於或等於結束日期");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("結束日期不能為空");

        RuleFor(x => x.Venue)
            .NotEmpty().WithMessage("場地不能為空");

        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("創建者不能為空");
    }
}