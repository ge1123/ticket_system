using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            try
            {
                var ticket = await _ticketService.CreateTicketAsync(createTicketDto);
                return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpGet("number/{ticketNumber}")]
        public async Task<ActionResult<Ticket>> GetTicketByNumber(string ticketNumber)
        {
            var ticket = await _ticketService.GetTicketByNumberAsync(ticketNumber);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAvailableTickets()
        {
            var tickets = await _ticketService.GetAvailableTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByCategory(Guid categoryId)
        {
            var tickets = await _ticketService.GetTicketsByCategoryAsync(categoryId);
            return Ok(tickets);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var tickets = await _ticketService.GetTicketsByDateRangeAsync(startDate, endDate);
            return Ok(tickets);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] CreateTicketDto updateTicketDto)
        {
            try
            {
                await _ticketService.UpdateTicketAsync(id, updateTicketDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                await _ticketService.DeleteTicketAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 