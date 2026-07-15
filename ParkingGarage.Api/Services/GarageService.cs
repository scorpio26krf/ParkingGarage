using ParkingGarage.Core.Models;
using ParkingGarage.Api.Repositories;

namespace ParkingGarage.Api.Services;

// Wraps garage operations for API use
public class GarageService
{
    private readonly Garage _garage;
    private readonly PricingRuleRepository _pricingRules;
    private readonly ReceiptRepository _receipts;

    // Active pricing rule stored in memory
    private PricingRule? _activeRule;

    public GarageService(
        Garage garage,
        PricingRuleRepository pricingRules,
        ReceiptRepository receipts)
    {
        _garage = garage;
        _pricingRules = pricingRules;
        _receipts = receipts;
    }

    public int GetAvailableSpaces()
        => _garage.Capacity - _garage.ParkedCars.Count;

    public int Capacity => _garage.Capacity;

    public IReadOnlyList<Car> GetParkedCars()
        => _garage.ParkedCars;

    public bool EnterCar(string licensePlate, DateTime now)
        => _garage.CarEnters(licensePlate, now);

    public Receipt ExitCar(string licensePlate)
    {
        var receipt = _garage.CarExits(licensePlate);

        // Persist receipt
        _receipts.AddAsync(receipt).GetAwaiter().GetResult();

        return receipt;
    }

    // Pricing rule operations
    public PricingRule CreatePricingRule(
        string label,
        decimal ratePerHour,
        int gracePeriodMinutes,
        decimal? maxDailyCharge)
    {
        var rule = new PricingRule(label, ratePerHour, gracePeriodMinutes, maxDailyCharge);

        // Persist rule
        _pricingRules.AddAsync(rule).GetAwaiter().GetResult();

        return rule;
    }

    public IReadOnlyList<PricingRule> GetPricingRules()
        => _pricingRules.GetAllAsync().GetAwaiter().GetResult();

    public void SetPricingRule(Guid id)
    {
        var rule = _pricingRules.GetByIdAsync(id).GetAwaiter().GetResult()
            ?? throw new InvalidOperationException("Pricing rule not found.");

        _activeRule = rule;
        _garage.SetPricingRule(rule);
    }

    public PricingRule? GetActivePricingRule()
        => _activeRule;
}
