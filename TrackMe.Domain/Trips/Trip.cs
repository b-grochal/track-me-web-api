using Domain.Locations;
using Domain.Members;
using System.Collections.Generic;

namespace Domain.Trips
{
    public class Trip
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }

        public virtual ICollection<Location> SensorValues { get; set; }
    }
}
