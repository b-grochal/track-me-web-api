using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Models.DTOs.Trips
{
    public class TripSensorDataDto
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public string BasicUserEmail { get; set; }
        public IEnumerable<SensorDataDto> SensorData { get; set; }
    }
}
