using Microsoft.EntityFrameworkCore;
using ParkingGarage.Api.Data;
using ParkingGarage.Core.Models;

namespace ParkingGarage.Api.Repositories;

public class ReceiptRepository
{
    private readonly GarageDbContext _db;

    public ReceiptRepository(GarageDbContext db)
    {
        _db = db;
    }

    public async Task<Receipt> AddAsync(Receipt receipt)
    {
        _db.Receipts.Add(receipt);
        await _db.SaveChangesAsync();
        return receipt;
    }

    public async Task<List<Receipt>> GetAllAsync()
        => await _db.Receipts.ToListAsync();
}

