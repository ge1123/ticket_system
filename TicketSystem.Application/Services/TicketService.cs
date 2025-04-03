using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Application.Interfaces;
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
            // 驗證日期
            if (createTicketDto.StartDate >= createTicketDto.EndDate)
            {
                throw new ArgumentException("開始日期必須早於結束日期");
            }

            // 驗證數量
            if (createTicketDto.Quantity <= 0)
            {
                throw new ArgumentException("數量必須大於 0");
            }

            // 驗證價格
            if (createTicketDto.Price <= 0)
            {
                throw new ArgumentException("價格必須大於 0");
            }

            // 驗證分類
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

            // 建立票券
            var ticket = new Ticket
            {
                Title = createTicketDto.Title,
                Description = createTicketDto.Description,
                Price = createTicketDto.Price,
                Quantity = createTicketDto.Quantity,
                StartDate = createTicketDto.StartDate,
                EndDate = createTicketDto.EndDate,
                Status = TicketStatus.Active,
                TicketNumber = GenerateTicketNumber(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createTicketDto.CreatedBy,
                UpdatedBy = createTicketDto.UpdatedBy,
                Venue = createTicketDto.Venue,
            };

            // 新增票券
            await _ticketRepository.AddAsync(ticket);

            // 新增票券分類關聯
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

            // 驗證日期
            if (updateTicketDto.StartDate >= updateTicketDto.EndDate)
            {
                throw new ArgumentException("開始日期必須早於結束日期");
            }

            // 驗證數量
            if (updateTicketDto.Quantity <= 0)
            {
                throw new ArgumentException("數量必須大於 0");
            }

            // 驗證價格
            if (updateTicketDto.Price <= 0)
            {
                throw new ArgumentException("價格必須大於 0");
            }

            // 驗證分類
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

            // 更新票券
            ticket.Title = updateTicketDto.Title;
            ticket.Description = updateTicketDto.Description;
            ticket.Price = updateTicketDto.Price;
            ticket.Quantity = updateTicketDto.Quantity;
            ticket.StartDate = updateTicketDto.StartDate;
            ticket.EndDate = updateTicketDto.EndDate;
            ticket.UpdatedAt = DateTime.UtcNow;

            // 更新票券分類關聯
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

        private string GenerateTicketNumber()
        {
            return $"TKT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
} 