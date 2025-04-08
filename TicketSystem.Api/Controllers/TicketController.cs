using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Commands.Tickets.CreateTicket;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Application.Interfaces;
using TicketSystem.Application.Queries.Tickets.GetTicketById;
using TicketSystem.Application.Queries.Tickets.GetTicketByNumber;
using TicketSystem.Domain.Aggregates.Ticket;

namespace TicketSystem.Api.Controllers
{
    /// <summary>
    /// 票券管理控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMediator _mediator;

        public TicketController(ITicketService ticketService, IMediator mediator)
        {
            _ticketService = ticketService;
            _mediator = mediator;
        }

        /// <summary>
        /// 建立新票券
        /// </summary>
        /// <param name="command">票券建立資料</param>
        /// <returns>新建立的票券資訊</returns>
        /// <response code="201">成功建立票券</response>
        /// <response code="400">請求資料驗證失敗</response>
        [HttpPost]
        [ProducesResponseType(typeof(Ticket), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateTicket([FromBody] CreateTicketCommand command)
            {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 取得指定 ID 的票券
        /// </summary>
        /// <param name="id">票券 ID</param>
        /// <returns>票券詳細資訊</returns>
        /// <response code="200">成功取得票券</response>
        /// <response code="404">找不到指定票券</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ticket), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid id)
        {
            try
            {
                var query = new GetTicketByIdQuery { Id = id };
                var ticket = await _mediator.Send(query);
                return Ok(ticket);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 依票券號碼取得票券
        /// </summary>
        /// <param name="ticketNumber">票券號碼</param>
        /// <returns>票券詳細資訊</returns>
        /// <response code="200">成功取得票券</response>
        /// <response code="404">找不到指定票券</response>
        [HttpGet("number/{ticketNumber}")]
        [ProducesResponseType(typeof(Ticket), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Ticket>> GetTicketByNumber(string ticketNumber)
        {
            try
            {
                var query = new GetTicketByNumberQuery { TicketNumber = ticketNumber };
                var ticket = await _mediator.Send(query);
                return Ok(ticket);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 取得所有票券
        /// </summary>
        /// <returns>票券列表</returns>
        /// <response code="200">成功取得票券列表</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Ticket>), 200)]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        /// <summary>
        /// 取得所有可用票券
        /// </summary>
        /// <returns>可用票券列表</returns>
        /// <response code="200">成功取得可用票券列表</response>
        [HttpGet("available")]
        [ProducesResponseType(typeof(IEnumerable<Ticket>), 200)]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAvailableTickets()
        {
            var tickets = await _ticketService.GetAvailableTicketsAsync();
            return Ok(tickets);
        }

        /// <summary>
        /// 取得指定分類的所有票券
        /// </summary>
        /// <param name="categoryId">分類 ID</param>
        /// <returns>票券列表</returns>
        /// <response code="200">成功取得分類票券列表</response>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(IEnumerable<Ticket>), 200)]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByCategory(Guid categoryId)
        {
            var tickets = await _ticketService.GetTicketsByCategoryAsync(categoryId);
            return Ok(tickets);
        }

        /// <summary>
        /// 取得指定日期範圍內的票券
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns>票券列表</returns>
        /// <response code="200">成功取得日期範圍內的票券列表</response>
        [HttpGet("date-range")]
        [ProducesResponseType(typeof(IEnumerable<Ticket>), 200)]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var tickets = await _ticketService.GetTicketsByDateRangeAsync(startDate, endDate);
            return Ok(tickets);
        }

        /// <summary>
        /// 更新票券資訊
        /// </summary>
        /// <param name="id">票券 ID</param>
        /// <param name="updateTicketDto">票券更新資料</param>
        /// <returns>無內容</returns>
        /// <response code="204">成功更新票券</response>
        /// <response code="400">請求資料驗證失敗</response>
        /// <response code="404">找不到指定票券</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] CreateTicketDto updateTicketDto)
            {
                await _ticketService.UpdateTicketAsync(id, updateTicketDto);
                return NoContent();
        }

        /// <summary>
        /// 刪除票券
        /// </summary>
        /// <param name="id">票券 ID</param>
        /// <returns>無內容</returns>
        /// <response code="204">成功刪除票券</response>
        /// <response code="404">找不到指定票券</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTicket(Guid id)
            {
                await _ticketService.DeleteTicketAsync(id);
                return NoContent();
        }
    }
}