using ParkingGarage.Core.Models;

namespace ParkingGarage.Tests;

public class CarTests
{
    [Test]
    public void CarCreation_SetsLicensePlateAndEnteredAt()
    {
        var now = DateTime.UtcNow;
        var car = new Car("ABC123", now);

        Assert.Multiple(() =>
        {
            Assert.That(car.LicensePlate, Is.EqualTo("ABC123"));
            Assert.That(car.EnteredAt, Is.EqualTo(now));
        });
        Assert.That(car.ExitedAt, Is.Null);
    }

    [Test]
    public void Exit_SetsExitedAt()
    {
        var enterTime = DateTime.UtcNow;
        var exitTime = enterTime.AddMinutes(20);

        var car = new Car("XYZ789", enterTime);
        car.Exit(exitTime);

        Assert.That(car.ExitedAt, Is.EqualTo(exitTime));
    }

    [Test]
    public void CarCreation_LeavesExitedAtNull()
    {
        var now = DateTime.UtcNow;
        var car = new Car("NOEXIT", now);

        Assert.That(car.ExitedAt, Is.Null);
    }
}

