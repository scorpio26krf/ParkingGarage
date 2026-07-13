namespace ParkingGarage.Api.DTOs;

// Returned when listing or creating pricing rules
public record PricingRuleResponse(
    Guid Id,
    string Label,
    decimal RatePerHour,
    int GracePeriodMinutes,
    decimal? MaxDailyCharge
);

