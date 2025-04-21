using Domain.ApplicationUsers;
using Domain.Trips;
using System.Collections.Generic;

namespace Domain.Members;

public class Member : ApplicationUser
{
    public virtual ICollection<Trip> Trips { get; set; }
}
