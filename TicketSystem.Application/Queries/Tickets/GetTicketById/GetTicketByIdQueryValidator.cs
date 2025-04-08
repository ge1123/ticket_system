using FluentValidation;

namespace TicketSystem.Application.Queries.Tickets.GetTicketById
{
    /// <summary>
    /// 獲取指定 ID 的票券查詢驗證器
    /// </summary>
    public class GetTicketByIdQueryValidator : AbstractValidator<GetTicketByIdQuery>
    {
        /// <summary>
        /// 初始化驗證器
        /// </summary>
        public GetTicketByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("票券 ID 不能為空");
        }
    }
} 