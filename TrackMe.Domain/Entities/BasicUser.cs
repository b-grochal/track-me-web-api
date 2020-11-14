using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMeWebAPI.Models
{
    public class BasicUser
    {
        public int BasicUserId { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
