using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMeWebAPI.Models
{
    public class SensorValues
    {
        public int ID { get; set; }
        public int TripID { get; set; }
        public DateTime UploadDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
