using ParkingGarage.Core.Models;

namespace ParkingGarage.Tests;

public class ReceiptTests
{
    [Test]
    public void Receipt_StoresAllValues()
    {
        var entry = DateTime.UtcNow;
        var exit = entry.AddHours(2);

        var receipt = new Receipt(
            Guid.NewGuid(),
            entry,
            exit,
            2m,
            10m,
            "Standard"
        );

        Assert.Multiple(() =>
        {
            Assert.That(receipt.EntryTime, Is.EqualTo(entry));
            Assert.That(receipt.ExitTime, Is.EqualTo(exit));
            Assert.That(receipt.TotalHours, Is.EqualTo(2m));
            Assert.That(receipt.TotalAmount, Is.EqualTo(10m));
            Assert.That(receipt.AppliedRuleLabel, Is.EqualTo("Standard"));
        });
    }

    [Test]
    public void Receipt_Throws_WhenExitBeforeEntry()
    {
        var entry = DateTime.UtcNow;
        var exit = entry.AddHours(-1);

        Assert.Throws<ArgumentException>(() =>
            new Receipt(Guid.NewGuid(), entry, exit, 1m, 5m, "Standard"));
    }
}

