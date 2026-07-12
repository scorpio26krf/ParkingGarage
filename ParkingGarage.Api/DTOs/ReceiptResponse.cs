namespace ParkingGarage.Api.DTOs;

public record ReceiptResponse(
    string LicensePlate,
    DateTime EnteredAt,
    DateTime ExitedAt,
    int DurationMinutes,
    decimal TotalPrice
);
