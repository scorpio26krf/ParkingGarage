using Microsoft.EntityFrameworkCore;
using ParkingGarage.Core.Models;

namespace ParkingGarage.Api.Data
{
    public class GarageDbContext : DbContext
    {
        public GarageDbContext(DbContextOptions<GarageDbContext> options) : base(options)
        {
        }
        public DbSet<PricingRule> PricingRules => Set<PricingRule>();
        public DbSet<Receipt> Receipts => Set<Receipt>();
    }
}
