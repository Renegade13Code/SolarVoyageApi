using Microsoft.AspNetCore.Mvc;
using SolarVoyage.Core.Services.Ships;
using SolarVoyage.API.DtoModels;

namespace SolarVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipController : ControllerBase
{
    private readonly IShipService _shipService;

    public ShipController(IShipService shipService)
    {
        _shipService = shipService;
    }

    [HttpGet]
    public async Task<IResult> GetAllShipsAsync()
    {
        var ships = await _shipService.GetAllAsync().ConfigureAwait(false);
        return Results.Ok(ships);
    }
    
    
    [HttpPost]
    public async Task<IResult> AddShipAsync([FromBody] Ship shipEntity)
    {
        try
        {
            var shipCore = new Core.Models.Domain.Ship()
            {
                Acceleration = shipEntity.Acceleration,
                Name = shipEntity.Name,
                Range = shipEntity.Range,
                Weight = shipEntity.Weight,
                CargoCapacity = shipEntity.CargoCapacity,
                TopSpeed = shipEntity.TopSpeed,
                PersonalCapacity = shipEntity.PersonalCapacity
            };
            await _shipService.AddAsync(shipCore).ConfigureAwait(false);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(statusCode: 500, detail:ex.Message);
        }
    }
}