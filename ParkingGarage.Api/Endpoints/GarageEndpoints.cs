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

        // Exit a car
        app.MapPost("/exit", ([FromBody] ExitRequest request, GarageService service) =>
        {
            var now = DateTime.UtcNow;

            try
            {
                var receipt = service.ExitCar(request.LicensePlate, now);

                return Results.Ok(new ReceiptResponse(
                    receipt.LicensePlate,
                    receipt.EnteredAt,
                    receipt.ExitedAt,
                    receipt.DurationMinutes,
                    receipt.TotalPrice
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
    }
}
