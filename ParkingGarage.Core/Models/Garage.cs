namespace ParkingGarage.Core.Models;

// Represents the parking garage and manages all parking operations.
public class Garage
{
    private readonly List<Car> _cars = new();
    public int Capacity { get; }

    public Garage(int capacity)
    {
        Capacity = capacity;
    }

    // Current parked cars (read-only to protect internal state)
    public IReadOnlyList<Car> ParkedCars => _cars;

    // Handles car entry rules: capacity + duplicate plates
    public bool CarEnters(string licensePlate, DateTime now)
    {
        if (_cars.Count >= Capacity)
            return false;

        if (_cars.Any(c => c.LicensePlate == licensePlate))
            return false;

        _cars.Add(new Car(licensePlate, now));
        return true;
    }

    // Handles car exit and creates a receipt
    public ParkingReceipt CarExits(string licensePlate, DateTime now)
    {
        var car = _cars.FirstOrDefault(c => c.LicensePlate == licensePlate) ?? throw new InvalidOperationException("Car not found.");
        car.Exit(now);

        var duration = Math.Max(1, (int)(now - car.EnteredAt).TotalMinutes);
        var price = CalculatePrice(duration);

        _cars.Remove(car);

        return new ParkingReceipt(
            car.LicensePlate,
            car.EnteredAt,
            now,
            duration,
            price
        );
    }

    private static decimal CalculatePrice(int durationMinutes)
    {
        return 5m + durationMinutes * 0.10m;
    }

    // Used only for test isolation
    public void Reset()
    {
        _cars.Clear();
    }
}
