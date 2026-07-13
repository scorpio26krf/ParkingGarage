using ParkingGarage.Core.Models;

namespace ParkingGarage.Tests;

public class PricingRuleTests
{
    [Test]
    public void PricingRule_CreatesWithValidValues()
    {
        var rule = new PricingRule(
            label: "Standard",
            ratePerHour: 5m,
            gracePeriodMinutes: 10,
            maxDailyCharge: 20m
        );

        Assert.Multiple(() =>
        {
            Assert.That(rule.Label, Is.EqualTo("Standard"));
            Assert.That(rule.RatePerHour, Is.EqualTo(5m));
            Assert.That(rule.GracePeriodMinutes, Is.EqualTo(10));
            Assert.That(rule.MaxDailyCharge, Is.EqualTo(20m));
        });
    }

    [Test]
    public void PricingRule_Throws_WhenLabelMissing()
    {
        Assert.Throws<ArgumentException>(() =>
            new PricingRule("", 5m, 10));
    }

    [Test]
    public void PricingRule_Throws_WhenRateNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PricingRule("BadRate", -1m, 10));
    }

    [Test]
    public void PricingRule_Throws_WhenGracePeriodNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PricingRule("BadGrace", 5m, -5));
    }

    [Test]
    public void PricingRule_Throws_WhenMaxDailyChargeNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PricingRule("BadMax", 5m, 10, -1m));
    }

    [Test]
    public void PricingRule_AllowsNullMaxDailyCharge()
    {
        var rule = new PricingRule("NoMax", 5m, 10, null);

        Assert.That(rule.MaxDailyCharge, Is.Null);
    }
}

