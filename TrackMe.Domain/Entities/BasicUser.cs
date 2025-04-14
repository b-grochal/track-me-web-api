using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Trips;

namespace TrackMe.Domain.Entities
{
    public class BasicUser : ApplicationUser
    {
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
