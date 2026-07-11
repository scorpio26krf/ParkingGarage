using ParkingGarage.Core.Models;

namespace ParkingGarage.Tests;

public class ParkingReceiptTests
{
    [Test]
    public void ConstructorSetsAllPropertiesCorrectly()
    {
        var license = "ABC123";
        var entered = new DateTime(2026, 6, 29, 12, 0, 0);
        var exited = new DateTime(2026, 6, 29, 14, 30, 0);
        var duration = 150;
        var price = 20.50m;

        var receipt = new ParkingReceipt(license, entered, exited, duration, price);

        Assert.Multiple(() =>
        {
            Assert.That(receipt.LicensePlate, Is.EqualTo(license));
            Assert.That(receipt.EnteredAt, Is.EqualTo(entered));
            Assert.That(receipt.ExitedAt, Is.EqualTo(exited));
            Assert.That(receipt.DurationMinutes, Is.EqualTo(duration));
            Assert.That(receipt.TotalPrice, Is.EqualTo(price));
        });
    }

    [Test]
    public void DurationMinutesIsAccurate()
    {
        var entered = new DateTime(2026, 6, 29, 12, 0, 0);
        var exited = entered.AddMinutes(45);

        var receipt = new ParkingReceipt("XYZ789", entered, exited, 45, 10m);

        Assert.That(receipt.DurationMinutes, Is.EqualTo(45));
    }

    [Test]
    public void TotalPriceIsStoredCorrectly()
    {
        var receipt = new ParkingReceipt("PRICE1",
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(30),
            30,
            8.00m);

        Assert.That(receipt.TotalPrice, Is.EqualTo(8.00m));
    }
}

