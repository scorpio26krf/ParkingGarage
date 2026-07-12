namespace ParkingGarage.Core.Models;

// Returned when a car exits the garage.
public class ParkingReceipt
{
    public string LicensePlate { get; }
    public DateTime EnteredAt { get; }
    public DateTime ExitedAt { get; }
    public int DurationMinutes { get; }
    public decimal TotalPrice { get; }

    public ParkingReceipt(string licensePlate, DateTime enteredAt, DateTime exitedAt, int durationMinutes, decimal totalPrice)
    {
        LicensePlate = licensePlate;
        EnteredAt = enteredAt;
        ExitedAt = exitedAt;
        DurationMinutes = durationMinutes;
        TotalPrice = totalPrice;
    }
}

