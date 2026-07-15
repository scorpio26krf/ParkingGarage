using Microsoft.EntityFrameworkCore;
using ParkingGarage.Api.Data;
using ParkingGarage.Core.Models;

namespace ParkingGarage.Api.Repositories;

public class PricingRuleRepository
{
    private readonly GarageDbContext _db;

    public PricingRuleRepository(GarageDbContext db)
    {
        _db = db;
    }

    public async Task<PricingRule> AddAsync(PricingRule rule)
    {
        _db.PricingRules.Add(rule);
        await _db.SaveChangesAsync();
        return rule;
    }

    public async Task<List<PricingRule>> GetAllAsync()
        => await _db.PricingRules.ToListAsync();

    public async Task<PricingRule?> GetByIdAsync(Guid id)
        => await _db.PricingRules.FindAsync(id);
}

