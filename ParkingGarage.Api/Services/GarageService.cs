using ParkingGarage.Core;
using ParkingGarage.Core.Models;

namespace ParkingGarage.Api.Services;

// Wraps garage operations for API use
public class GarageService
{
    private readonly Garage _garage;
    private readonly List<PricingRule> _pricingRules = new(); // temporary in-memory list

    public GarageService(Garage garage)
    {
        _garage = garage;
    }

    public int GetAvailableSpaces()
        => _garage.Capacity - _garage.ParkedCars.Count;

    public int Capacity => _garage.Capacity;

    public IReadOnlyList<Car> GetParkedCars()
        => _garage.ParkedCars;

    public bool EnterCar(string licensePlate, DateTime now)
        => _garage.CarEnters(licensePlate, now);

    public Receipt ExitCar(string licensePlate)
        => _garage.CarExits(licensePlate);

    // Pricing rule operations
    public PricingRule CreatePricingRule(string label, decimal ratePerHour, int gracePeriodMinutes, decimal? maxDailyCharge)
    {
        var rule = new PricingRule(label, ratePerHour, gracePeriodMinutes, maxDailyCharge);
        _pricingRules.Add(rule);
        return rule;
    }

    public IReadOnlyList<PricingRule> GetPricingRules()
        => _pricingRules;

    public void SetPricingRule(Guid id)
    {
        var rule = _pricingRules.FirstOrDefault(r => r.Id == id)
            ?? throw new InvalidOperationException("Pricing rule not found.");

        _garage.SetPricingRule(rule);
    }
}
