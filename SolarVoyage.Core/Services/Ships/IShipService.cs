using SolarVoyage.Core.Models.Domain;

namespace SolarVoyage.Core.Services.Ships;

public interface IShipService
{
    public Task AddAsync(Ship ship);
    public Task<IEnumerable<Ship>> GetAllAsync();

}