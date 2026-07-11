using ParkingGarage.Core.Models;

namespace ParkingGarage.Tests;

public class GarageTests
{
    [Test]
    public void CarEnters_WhenGarageHasSpace_ReturnsTrue()
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
    public void CarEnters_WhenGarageIsFull_ReturnsFalse()
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
    public void CarEnters_WhenLicensePlateAlreadyExists_ReturnsFalse()
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
    public void CarExits_WhenCarExists_ReturnsReceiptAndRemovesCar()
    {
        var garage = new Garage(capacity: 5);
        var enterTime = DateTime.UtcNow;
        var exitTime = enterTime.AddMinutes(30);

        garage.CarEnters("EXITME", enterTime);

        var receipt = garage.CarExits("EXITME", exitTime);

        Assert.Multiple(() =>
        {
            Assert.That(receipt.LicensePlate, Is.EqualTo("EXITME"));
            Assert.That(receipt.EnteredAt, Is.EqualTo(enterTime));
            Assert.That(receipt.ExitedAt, Is.EqualTo(exitTime));
            Assert.That(receipt.DurationMinutes, Is.EqualTo(30));
            Assert.That(receipt.TotalPrice, Is.EqualTo(5m + (30 * 0.10m)));
            Assert.That(garage.ParkedCars, Is.Empty);
        });
    }

    [Test]
    public void CarExits_WhenCarDoesNotExist_ThrowsException()
    {
        var garage = new Garage(capacity: 5);
        var now = DateTime.UtcNow;

        Assert.Throws<InvalidOperationException>(() =>
        {
            garage.CarExits("NOTFOUND", now);
        });
    }
}