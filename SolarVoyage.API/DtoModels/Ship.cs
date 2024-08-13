namespace SolarVoyage.API.DtoModels;

public class Ship
{
    public string? Name { get; set; }

    public string? PersonalCapacity { get; set; }

    public string? CargoCapacity { get; set; }

    public int? Range { get; set; }

    public int? Weight { get; set; }

    public int? TopSpeed { get; set; }

    public int? Acceleration { get; set; }
}