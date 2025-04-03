using System;
using System.Collections.Generic;

namespace TicketSystem.Application.DTOs.Ticket
{
    public class CreateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Venue { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
} 