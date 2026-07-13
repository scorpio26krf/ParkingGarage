namespace ParkingGarage.Core.Models;

// Represents the final charge when a car exits
public class Receipt
{
    public Guid Id { get; }
    public Guid TicketId { get; }
    public DateTime EntryTime { get; }
    public DateTime ExitTime { get; }
    public decimal TotalHours { get; }
    public decimal TotalAmount { get; }
    public string AppliedRuleLabel { get; }

    public Receipt(
        Guid ticketId,
        DateTime entryTime,
        DateTime exitTime,
        decimal totalHours,
        decimal totalAmount,
        string appliedRuleLabel)
    {
        if (ticketId == Guid.Empty)
            throw new ArgumentException("TicketId is required.", nameof(ticketId));

        if (exitTime < entryTime)
            throw new ArgumentException("Exit time cannot be before entry time.", nameof(exitTime));

        ArgumentOutOfRangeException.ThrowIfNegative(totalHours);

        ArgumentOutOfRangeException.ThrowIfNegative(totalAmount);

        if (string.IsNullOrWhiteSpace(appliedRuleLabel))
            throw new ArgumentException("Applied rule label is required.", nameof(appliedRuleLabel));

        Id = Guid.NewGuid();
        TicketId = ticketId;
        EntryTime = entryTime;
        ExitTime = exitTime;
        TotalHours = totalHours;
        TotalAmount = totalAmount;
        AppliedRuleLabel = appliedRuleLabel;
    }
}