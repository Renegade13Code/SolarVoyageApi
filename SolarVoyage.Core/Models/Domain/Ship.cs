using System;
using System.Collections.Generic;

namespace SolarVoyage.Core.Models.Domain;

public partial class Ship
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? PersonalCapacity { get; set; }

    public string? CargoCapacity { get; set; }

    public int? Range { get; set; }

    public int? Weight { get; set; }

    public int? TopSpeed { get; set; }

    public int? Acceleration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
