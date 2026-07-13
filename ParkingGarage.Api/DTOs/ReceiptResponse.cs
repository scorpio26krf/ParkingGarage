namespace ParkingGarage.Api.DTOs;

// Returned when a car exits the garage
public record ReceiptResponse(
    Guid Id,
    DateTime EntryTime,
    DateTime ExitTime,
    decimal TotalHours,
    decimal TotalAmount,
    string AppliedRuleLabel
);

