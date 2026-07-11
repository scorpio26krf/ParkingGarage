namespace ParkingGarage.Core.Models;

public class Garage
{
    private readonly List<Car> _cars = new();
    public int Capacity { get; }

    public Garage(int capacity)
    {
        Capacity = capacity;
    }

    public IReadOnlyList<Car> ParkedCars => _cars;

    public bool CarEnters(string licensePlate, DateTime now)
    {
        if (_cars.Count >= Capacity)
            return false;

        if (_cars.Any(c => c.LicensePlate == licensePlate))
            return false;

        _cars.Add(new Car(licensePlate, now));
        return true;
    }

    public ParkingReceipt CarExits(string licensePlate, DateTime now)
    {
        var car = _cars.FirstOrDefault(c => c.LicensePlate == licensePlate);
        if (car == null)
            throw new InvalidOperationException("Car not found.");

        car.Exit(now);

        var duration = (int)(now - car.EnteredAt).TotalMinutes;
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
}
