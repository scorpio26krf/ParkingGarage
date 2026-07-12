using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using ParkingGarage.Api.DTOs;

namespace ParkingGarage.Tests;

public class GarageApiTests
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public GarageApiTests()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [SetUp]
    public void ResetGarage()
    {
        var garage = _factory.Services.GetRequiredService<Core.Models.Garage>();
        garage.Reset();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task CarEntry_ReturnsOk_WhenGarageHasSpace()
    {
        var request = new EnterRequest("API123");

        var response = await _client.PostAsJsonAsync("/enter", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task CarEntry_ReturnsBadRequest_WhenDuplicatePlate()
    {
        var request = new EnterRequest("DUPLICATE");

        await _client.PostAsJsonAsync("/enter", request);
        var response = await _client.PostAsJsonAsync("/enter", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CarExit_ReturnsReceipt_WhenCarExists()
    {
        await _client.PostAsJsonAsync("/enter", new EnterRequest("EXITME"));

        var response = await _client.PostAsJsonAsync("/exit", new ExitRequest("EXITME"));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var receipt = await response.Content.ReadFromJsonAsync<ReceiptResponse>();

        Assert.Multiple(() =>
        {
            Assert.That(receipt!.LicensePlate, Is.EqualTo("EXITME"));
            Assert.That(receipt.DurationMinutes, Is.GreaterThan(0));
            Assert.That(receipt.TotalPrice, Is.GreaterThan(0));
        });
    }

    [Test]
    public async Task CarExit_ReturnsNotFound_WhenCarDoesNotExist()
    {
        var response = await _client.PostAsJsonAsync("/exit", new ExitRequest("NOTFOUND"));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task CarsEndpoint_ReturnsParkedCars()
    {
        await _client.PostAsJsonAsync("/enter", new EnterRequest("CAR1"));
        await _client.PostAsJsonAsync("/enter", new EnterRequest("CAR2"));

        var response = await _client.GetAsync("/cars");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var cars = await response.Content.ReadFromJsonAsync<List<string>>();

        Assert.Multiple(() =>
        {
            Assert.That(cars, Is.Not.Null);
            Assert.That(cars!.Count, Is.EqualTo(2));
            Assert.That(cars, Does.Contain("CAR1"));
            Assert.That(cars, Does.Contain("CAR2"));
        });
    }

    [Test]
    public async Task SpacesEndpoint_ReturnsAvailableSpaces()
    {
        await _client.PostAsJsonAsync("/enter", new EnterRequest("A"));
        await _client.PostAsJsonAsync("/enter", new EnterRequest("B"));
        await _client.PostAsJsonAsync("/enter", new EnterRequest("C"));

        var response = await _client.GetAsync("/spaces");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ContainsKey("spaces"), Is.True);
            Assert.That(result["spaces"], Is.EqualTo(47));
        });
    }

    [Test]
    public async Task StatusEndpoint_ReturnsGarageSummary()
    {
        await _client.PostAsJsonAsync("/enter", new EnterRequest("STATUSCAR"));

        var response = await _client.GetAsync("/status");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var status = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();

        Assert.Multiple(() =>
        {
            Assert.That(status, Is.Not.Null);
            Assert.That(status!.ContainsKey("capacity"), Is.True);
            Assert.That(status.ContainsKey("parked"), Is.True);
            Assert.That(status.ContainsKey("available"), Is.True);

            Assert.That(status["capacity"], Is.EqualTo(50));
            Assert.That(status["parked"], Is.EqualTo(1));
            Assert.That(status["available"], Is.EqualTo(49));
        });
    }
}
