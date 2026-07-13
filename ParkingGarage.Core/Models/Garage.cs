namespace ParkingGarage.Core.Models;

// Represents the parking garage and manages all parking operations.
public class Garage
{
    private readonly List<Car> _cars = new();      // cars currently parked
    private PricingRule? _pricingRule;             // pricing configuration

    public int Capacity { get; }

    public Garage(int capacity)
    {
        Capacity = capacity;
    }

    // Allows the app to configure how pricing works
    public void SetPricingRule(PricingRule rule)
    {
        _pricingRule = rule ?? throw new ArgumentNullException(nameof(rule));
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
    public Receipt CarExits(string licensePlate)
    {
        if (_pricingRule is null)
            throw new InvalidOperationException("Pricing rule has not been configured.");

        var car = _cars.FirstOrDefault(c => c.LicensePlate == licensePlate)
            ?? throw new InvalidOperationException("Car not found.");

        var exitTime = DateTime.UtcNow;
        var duration = exitTime - car.EnteredAt;

        // Round up for billing
        var totalHours = (decimal)duration.TotalHours;

        decimal totalAmount = 0;

        // Apply pricing only if grace period is exceeded
        if (duration.TotalMinutes > _pricingRule.GracePeriodMinutes)
        {
            totalAmount = Math.Ceiling(totalHours) * _pricingRule.RatePerHour;

            // Apply daily cap if configured
            if (_pricingRule.MaxDailyCharge.HasValue)
            {
                totalAmount = Math.Min(totalAmount, _pricingRule.MaxDailyCharge.Value);
            }
        }

        var receipt = new Receipt(
            Guid.NewGuid(),
            car.EnteredAt,
            exitTime,
            totalHours,
            totalAmount,
            _pricingRule.Label
        );

        // Mark the car as exited
        car.Exit(exitTime);

        // Remove the car from the garage
        _cars.Remove(car);

        return receipt;
    }

    // Used only for test isolation
    public void Reset()
    {
        _cars.Clear();
    }
}
