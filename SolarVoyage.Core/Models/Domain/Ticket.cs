using System;
using System.Collections.Generic;

namespace SolarVoyage.Core.Models.Domain;

public partial class Ticket
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? FlightId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Flight? Flight { get; set; }

    public virtual User? User { get; set; }
}
