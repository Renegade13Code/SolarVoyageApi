using System;
using System.Collections.Generic;

namespace SolarVoyage.Core.Models.Domain;

/// <summary>
/// Flights can potentialy have refuleing stops across the solar system
/// </summary>
public partial class Flight
{
    public Guid Id { get; set; }

    public string? FlightNumber { get; set; }

    public Guid? ShipId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    /// <summary>
    /// The location of the launchpad used for departure
    /// </summary>
    public string? LaunchpadDepature { get; set; }

    public string? LaunchpadArrival { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<CargoItem> CargoItems { get; set; } = new List<CargoItem>();

    public virtual Ship? Ship { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
