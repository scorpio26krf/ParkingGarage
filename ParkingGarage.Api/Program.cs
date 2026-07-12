using ParkingGarage.Api.Endpoints;
using ParkingGarage.Api.Services;
using ParkingGarage.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Services.AddSingleton(new Garage(capacity: 50));
builder.Services.AddScoped<GarageService>();

var app = builder.Build();

// Map API endpoints
app.MapGarageEndpoints();

app.Run();

// Required for WebApplicationFactory in API tests
public partial class Program { }
