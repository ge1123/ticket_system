using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Application.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(CreateTicketDto createTicketDto);
        Task<Ticket> GetTicketByIdAsync(Guid id);
        Task<Ticket> GetTicketByNumberAsync(string ticketNumber);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<IEnumerable<Ticket>> GetAvailableTicketsAsync();
        Task<IEnumerable<Ticket>> GetTicketsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Ticket>> GetTicketsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task UpdateTicketAsync(Guid id, CreateTicketDto updateTicketDto);
        Task DeleteTicketAsync(Guid id);
    }
} 