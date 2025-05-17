using Domain.Locations;
using Domain.Members;
using System.Collections.Generic;

namespace Domain.Trips;

public class Trip
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int MemberId { get; set; }

    public Member Member { get; set; } = null!;

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
