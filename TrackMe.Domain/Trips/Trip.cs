using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;

namespace Domain.Trips
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public string BasicUserId { get; set; }
        public virtual BasicUser BassicUser { get; set; }
        public virtual ICollection<Location> SensorValues { get; set; }
    }
}
