using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;
using TicketSystem.Domain.Enums;

namespace TicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TicketService(ITicketRepository ticketRepository, ICategoryRepository categoryRepository)
        {
            _ticketRepository = ticketRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Ticket> CreateTicketAsync(CreateTicketDto createTicketDto)
        {
            // 驗證分類是否存在
            if (createTicketDto.CategoryIds != null && createTicketDto.CategoryIds.Any())
            {
                foreach (var categoryId in createTicketDto.CategoryIds)
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
                createTicketDto.Title,
                createTicketDto.Description,
                createTicketDto.Price,
                createTicketDto.Quantity,
                createTicketDto.StartDate,
                createTicketDto.EndDate,
                createTicketDto.Venue,
                createTicketDto.CreatedBy
            );

            // 新增票券
            await _ticketRepository.AddAsync(ticket);

            // 設定分類
            if (createTicketDto.CategoryIds != null && createTicketDto.CategoryIds.Any())
            {
                var ticketCategories = createTicketDto.CategoryIds.Select(categoryId =>
                    new TicketCategory
                    {
                        TicketId = ticket.Id,
                        CategoryId = categoryId
                    });

                ticket.Categories = ticketCategories.ToList();
            }

            return ticket;
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task<Ticket> GetTicketByNumberAsync(string ticketNumber)
        {
            return await _ticketRepository.GetByTicketNumberAsync(ticketNumber);
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAvailableTicketsAsync()
        {
            return await _ticketRepository.GetAvailableTicketsAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByCategoryAsync(Guid categoryId)
        {
            return await _ticketRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _ticketRepository.GetTicketsByDateRangeAsync(startDate, endDate);
        }

        public async Task UpdateTicketAsync(Guid id, CreateTicketDto updateTicketDto)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
            {
                throw new ArgumentException($"找不到 ID 為 {id} 的票券");
            }

            // 驗證分類是否存在
            if (updateTicketDto.CategoryIds != null && updateTicketDto.CategoryIds.Any())
            {
                foreach (var categoryId in updateTicketDto.CategoryIds)
                {
                    var category = await _categoryRepository.GetByIdAsync(categoryId);
                    if (category == null)
                    {
                        throw new ArgumentException($"找不到 ID 為 {categoryId} 的分類");
                    }
                }
            }

            // 使用 Entity 內部的 Update 方法
            ticket.Update(
                updateTicketDto.Title,
                updateTicketDto.Description,
                updateTicketDto.Price,
                updateTicketDto.Quantity,
                updateTicketDto.StartDate,
                updateTicketDto.EndDate,
                updateTicketDto.UpdatedBy
            );

            // 更新票券分類
            if (updateTicketDto.CategoryIds != null && updateTicketDto.CategoryIds.Any())
            {
                var ticketCategories = updateTicketDto.CategoryIds.Select(categoryId =>
                    new TicketCategory
                    {
                        TicketId = ticket.Id,
                        CategoryId = categoryId
                    });

                ticket.Categories = ticketCategories.ToList();
            }

            _ticketRepository.Update(ticket);
        }

        public async Task DeleteTicketAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
            {
                throw new ArgumentException($"找不到 ID 為 {id} 的票券");
            }

            _ticketRepository.Remove(ticket);
        }
    }
}