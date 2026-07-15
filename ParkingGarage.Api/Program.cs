using Microsoft.EntityFrameworkCore;
using ParkingGarage.Api.Data;
using ParkingGarage.Api.Endpoints;
using ParkingGarage.Api.Middleware;
using ParkingGarage.Api.Repositories;
using ParkingGarage.Api.Services;
using ParkingGarage.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Services.AddSingleton(new Garage(capacity: 50));

builder.Services.AddScoped<PricingRuleRepository>();
builder.Services.AddScoped<ReceiptRepository>();
builder.Services.AddScoped<GarageService>();

builder.Services.AddDbContext<GarageDbContext>(options => options.UseSqlite("Data Source=garage.db"));

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Map API endpoints
app.MapGarageEndpoints();

app.Run();

// Required for WebApplicationFactory in API tests
public partial class Program { }
