namespace ParkingGarage.Api.DTOs;

// Used when creating a new pricing rule
public record CreatePricingRuleRequest(
    string Label,
    decimal RatePerHour,
    int GracePeriodMinutes,
    decimal? MaxDailyCharge
);

