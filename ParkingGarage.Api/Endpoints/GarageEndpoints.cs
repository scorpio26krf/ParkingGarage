using Microsoft.AspNetCore.Mvc;
using ParkingGarage.Api.DTOs;
using ParkingGarage.Api.Services;

namespace ParkingGarage.Api.Endpoints;

// Defines all API endpoints for the garage.
public static class GarageEndpoints
{
    public static void MapGarageEndpoints(this IEndpointRouteBuilder app)
    {
        // Enter a car
        app.MapPost("/enter", ([FromBody] EnterRequest request, GarageService service) =>
        {
            var now = DateTime.UtcNow;
            var success = service.EnterCar(request.LicensePlate, now);

            return success
                ? Results.Ok()
                : Results.BadRequest("Garage full or car already parked.");
        });

        // Exit a car and return a receipt
        app.MapPost("/exit", ([FromBody] ExitRequest request, GarageService service) =>
        {
            try
            {
                var receipt = service.ExitCar(request.LicensePlate);

                return Results.Ok(new ReceiptResponse(
                    receipt.Id,
                    receipt.EntryTime,
                    receipt.ExitTime,
                    receipt.TotalHours,
                    receipt.TotalAmount,
                    receipt.AppliedRuleLabel
                ));
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound("Car not found.");
            }
        });

        // List parked cars
        app.MapGet("/cars", (GarageService service) =>
        {
            var cars = service.GetParkedCars()
                              .Select(c => c.LicensePlate)
                              .ToList();

            return Results.Ok(cars);
        });

        // Available spaces
        app.MapGet("/spaces", (GarageService service) =>
        {
            var spaces = service.GetAvailableSpaces();
            return Results.Ok(new { spaces });
        });

        // Garage status
        app.MapGet("/status", (GarageService service) =>
        {
            var status = new
            {
                capacity = service.Capacity,
                parked = service.GetParkedCars().Count,
                available = service.GetAvailableSpaces()
            };

            return Results.Ok(status);
        });

        // Create a pricing rule
        app.MapPost("/pricing-rules", ([FromBody] CreatePricingRuleRequest req, GarageService service) =>
        {
            var rule = service.CreatePricingRule(
                req.Label,
                req.RatePerHour,
                req.GracePeriodMinutes,
                req.MaxDailyCharge
            );

            return Results.Created($"/pricing-rules/{rule.Id}",
                new PricingRuleResponse(
                    rule.Id,
                    rule.Label,
                    rule.RatePerHour,
                    rule.GracePeriodMinutes,
                    rule.MaxDailyCharge
                ));
        });

        // List pricing rules
        app.MapGet("/pricing-rules", (GarageService service) =>
        {
            var rules = service.GetPricingRules()
                .Select(r => new PricingRuleResponse(
                    r.Id,
                    r.Label,
                    r.RatePerHour,
                    r.GracePeriodMinutes,
                    r.MaxDailyCharge
                ));

            return Results.Ok(rules);
        });

        // Set active pricing rule
        app.MapPost("/pricing-rules/{id:guid}/activate", (Guid id, GarageService service) =>
        {
            service.SetPricingRule(id);
            return Results.Ok();
        });
    }
}

