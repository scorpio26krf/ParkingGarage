using ParkingGarage.Core.Models;

namespace ParkingGarage.Tests;

public class GarageTests
{
    private Garage _garage;

    [SetUp]
    public void Setup()
    {
        _garage = new Garage(50);
    }

    [Test]
    public void CarEnters_AddsCar()
    {
        var result = _garage.CarEnters("ABC123", DateTime.UtcNow);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(_garage.ParkedCars, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void CarEntry_SucceedsWhenGarageHasSpace()
    {
        var garage = new Garage(capacity: 2);
        var now = DateTime.UtcNow;

        var result = garage.CarEnters("ABC123", now);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(garage.ParkedCars, Has.Count.EqualTo(1));
        });
        Assert.That(garage.ParkedCars[0].LicensePlate, Is.EqualTo("ABC123"));
    }

    [Test]
    public void CarEntry_FailsWhenGarageIsFull()
    {
        var garage = new Garage(capacity: 1);
        var now = DateTime.UtcNow;

        garage.CarEnters("CAR1", now);
        var result = garage.CarEnters("CAR2", now);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(garage.ParkedCars, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void CarEntry_FailsWhenLicensePlateAlreadyExists()
    {
        var garage = new Garage(capacity: 5);
        var now = DateTime.UtcNow;

        garage.CarEnters("DUPLICATE", now);
        var result = garage.CarEnters("DUPLICATE", now);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(garage.ParkedCars, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void CarExit_ReturnsReceiptAndRemovesCar()
    {
        var garage = new Garage(capacity: 5);
        var enterTime = DateTime.UtcNow;

        garage.CarEnters("EXITME", enterTime);

        var rule = new PricingRule("Standard", 5m, 0, null);
        garage.SetPricingRule(rule);

        var receipt = garage.CarExits("EXITME");

        Assert.Multiple(() =>
        {
            Assert.That(receipt.EntryTime, Is.EqualTo(enterTime));
            Assert.That(receipt.ExitTime, Is.GreaterThan(enterTime));
            Assert.That(receipt.TotalHours, Is.GreaterThan(0));
            Assert.That(receipt.TotalAmount, Is.GreaterThanOrEqualTo(5m));
            Assert.That(receipt.AppliedRuleLabel, Is.EqualTo("Standard"));
            Assert.That(garage.ParkedCars, Is.Empty);
        });
    }

    [Test]
    public void CarExit_ThrowsWhenCarNotFound()
    {
        var garage = new Garage(capacity: 5);

        Assert.Throws<InvalidOperationException>(() =>
        {
            garage.CarExits("NOTFOUND");
        });
    }
}