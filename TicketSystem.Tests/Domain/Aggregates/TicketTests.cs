using TicketSystem.Domain.Aggregates.Ticket;
using TicketSystem.Domain.Enums;

namespace TicketSystem.Tests.Domain.Aggregates;

public class TicketTests
{
    [Theory]
    [InlineData("演唱會門票", "五月天巡迴演唱會", 1500, 100, "小巨蛋", "admin")]
    [InlineData("音樂祭門票", "大港開唱", 1200, 50, "駁二", "user01")]
    public void Create_GivenValidInput_ShouldReturnTicketWithCorrectValues(
        string title,
        string description,
        decimal price,
        int quantity,
        string venue,
        string createdBy)
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(1);
        var endDate = DateTime.UtcNow.AddDays(2);

        // Act
        var ticket = Ticket.Create(title, description, price, quantity, startDate, endDate, venue, createdBy);

        // Assert
        Assert.NotNull(ticket);
        Assert.Equal(title, ticket.Title);
        Assert.Equal(description, ticket.Description);
        Assert.Equal(price, ticket.Price);
        Assert.Equal(quantity, ticket.Quantity);
        Assert.Equal(venue, ticket.Venue);
        Assert.Equal(TicketStatus.Active, ticket.Status);
    }

    [Fact]
    public void Update_GivenValidInput_ShouldUpdateTicketSuccessfully()
    {
        // Arrange
        var ticket = GetSampleTicket();

        var newTitle = "更新後標題";
        var newDescription = "更新後描述";
        var newPrice = 2000;
        var newQuantity = 99;
        var newStartDate = DateTime.UtcNow.AddDays(5);
        var newEndDate = DateTime.UtcNow.AddDays(6);
        var updatedBy = "editor";

        // Act
        ticket.Update(newTitle, newDescription, newPrice, newQuantity, newStartDate, newEndDate, updatedBy);

        // Assert
        Assert.Equal(newTitle, ticket.Title);
        Assert.Equal(newDescription, ticket.Description);
        Assert.Equal(newPrice, ticket.Price);
        Assert.Equal(newQuantity, ticket.Quantity);
        Assert.Equal(newStartDate, ticket.StartDate);
        Assert.Equal(newEndDate, ticket.EndDate);
        Assert.Equal(updatedBy, ticket.UpdatedBy);
        Assert.NotNull(ticket.UpdatedAt);
    }

    [Theory]
    [InlineData(-1, 10)] // 無效價格
    [InlineData(100, 0)] // 無效數量
    [InlineData(100, -5)] // 負數數量
    public void Update_GivenInvalidPriceOrQuantity_ShouldThrow(decimal price, int quantity)
    {
        // Arrange
        var ticket = GetSampleTicket();

        Assert.Throws<ArgumentException>(() =>
            ticket.Update("標題", "描述", price, quantity, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2),
                "editor"));
    }

    private Ticket GetSampleTicket()
    {
        return Ticket.Create(
            "原始標題", "原始描述", 1000, 50,
            DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2),
            "原始場地", "admin"
        );
    }
}