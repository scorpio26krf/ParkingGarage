namespace ParkingGarage.Core.Models;

// Represents the final charge when a car exits
public class Receipt
{
    public Guid Id { get; private set; }
    public Guid TicketId { get; private set; }
    public DateTime EntryTime { get; private set; }
    public DateTime ExitTime { get; private set; }
    public decimal TotalHours { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string AppliedRuleLabel { get; private set; } = null!;

    private Receipt() { } // EF Core

    public Receipt(Guid ticketId, DateTime entryTime, DateTime exitTime,
                   decimal totalHours, decimal totalAmount, string appliedRuleLabel)
    {
        if (ticketId == Guid.Empty)
            throw new ArgumentException("TicketId is required.", nameof(ticketId));

        if (exitTime < entryTime)
            throw new ArgumentException("Exit time cannot be before entry time.");

        if (totalHours < 0)
            throw new ArgumentOutOfRangeException(nameof(totalHours));

        if (totalAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(totalAmount));

        if (string.IsNullOrWhiteSpace(appliedRuleLabel))
            throw new ArgumentException("Applied rule label is required.");

        Id = Guid.NewGuid();
        TicketId = ticketId;
        EntryTime = entryTime;
        ExitTime = exitTime;
        TotalHours = totalHours;
        TotalAmount = totalAmount;
        AppliedRuleLabel = appliedRuleLabel;
    }
}
