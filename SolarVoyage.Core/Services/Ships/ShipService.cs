using Microsoft.EntityFrameworkCore;
using SolarVoyage.Core.Data;
using SolarVoyage.Core.Models.Domain;

namespace SolarVoyage.Core.Services.Ships;

public class ShipService: IShipService
{
    private readonly SolarVoyageContext _dbContext;

    public ShipService(SolarVoyageContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Ship shipEntity)
    {
        shipEntity.CreatedAt = DateTime.Now;
        shipEntity.LastUpdated = DateTime.Now;
        await _dbContext.Ships.AddAsync(shipEntity).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Ship>> GetAllAsync()
    {
        return await _dbContext.Ships.ToListAsync();
    }
}