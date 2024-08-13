using System;
using System.Collections.Generic;

namespace SolarVoyage.Core.Models.Domain;

public partial class User
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<CargoItem> CargoItems { get; set; } = new List<CargoItem>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
