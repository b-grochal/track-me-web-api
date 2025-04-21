using Domain.Locations;
using Domain.Members;
using System.Collections.Generic;

namespace Domain.Trips
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public string BasicUserId { get; set; }
        public virtual Member BassicUser { get; set; }
        public virtual ICollection<Location> SensorValues { get; set; }
    }
}
