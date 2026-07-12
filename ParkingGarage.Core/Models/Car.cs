namespace ParkingGarage.Core.Models;

// Represents a parked car with entry/exit timestamps.
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

    // Marks the car as exited
    public void Exit(DateTime exitTime)
    {
        ExitedAt = exitTime;
    }
}

