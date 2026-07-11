namespace ParkingGarage.Core.Models;

public class Car
{
    public string LicensePlate { get; }
    public DateTime EnteredAt { get; }
    public DateTime? ExitedAt { get; private set; }

    public Car(string licensePlate, DateTime enteredAt)
    {
        LicensePlate = licensePlate;
        EnteredAt = enteredAt;
        ExitedAt = null;
    }

    public void Exit(DateTime exitTime)
    {
        ExitedAt = exitTime;
    }
}

