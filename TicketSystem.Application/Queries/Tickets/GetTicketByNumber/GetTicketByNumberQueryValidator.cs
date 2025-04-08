using FluentValidation;

namespace TicketSystem.Application.Queries.Tickets.GetTicketByNumber
{
    /// <summary>
    /// 獲取指定票券號碼的票券查詢驗證器
    /// </summary>
    public class GetTicketByNumberQueryValidator : AbstractValidator<GetTicketByNumberQuery>
    {
        /// <summary>
        /// 初始化驗證器
        /// </summary>
        public GetTicketByNumberQueryValidator()
        {
            RuleFor(x => x.TicketNumber)
                .NotEmpty().WithMessage("票券號碼不能為空");
        }
    }
} 