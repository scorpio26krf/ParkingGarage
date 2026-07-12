using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ParkingGarage.Core.Models;

namespace ParkingGarage.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove existing Garage registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(Garage));

            if (descriptor != null)
                services.Remove(descriptor);

            // Add a fresh singleton Garage for each test run
            services.AddSingleton(new Garage(capacity: 50));
        });
    }
}


