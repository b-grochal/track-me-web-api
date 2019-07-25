using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMeWebAPI.Models
{
    public class Trip
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int BasicUserID { get; set; }
        public virtual ICollection<SensorValues> SensorValues { get; set; }
    }
}
