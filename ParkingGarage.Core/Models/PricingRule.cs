namespace ParkingGarage.Core.Models;

// Defines how the garage charges for parking
public class PricingRule
{
    public Guid Id { get; private set; }
    public string Label { get; private set; } = null!;
    public decimal RatePerHour { get; private set; }
    public int GracePeriodMinutes { get; private set; }
    public decimal? MaxDailyCharge { get; private set; }

    private PricingRule() { } // EF Core

    public PricingRule(string label, decimal ratePerHour, int gracePeriodMinutes, decimal? maxDailyCharge = null)
    {
        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentException("Label is required.", nameof(label));

        ArgumentOutOfRangeException.ThrowIfNegative(ratePerHour);
        ArgumentOutOfRangeException.ThrowIfNegative(gracePeriodMinutes);

        if (maxDailyCharge.HasValue && maxDailyCharge.Value < 0)
            throw new ArgumentOutOfRangeException(nameof(maxDailyCharge));

        Id = Guid.NewGuid();
        Label = label;
        RatePerHour = ratePerHour;
        GracePeriodMinutes = gracePeriodMinutes;
        MaxDailyCharge = maxDailyCharge;
    }
}
