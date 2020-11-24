using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMe.Domain.Entities
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public string BasicUserId { get; set; }
        public BasicUser BassicUser { get; set; }
        public virtual ICollection<SensorData> SensorValues { get; set; }
    }
}
