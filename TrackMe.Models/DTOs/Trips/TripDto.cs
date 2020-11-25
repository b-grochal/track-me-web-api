using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Models.DTOs.Trips
{
    public class TripDto
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public string BasicUserEmail { get; set; }
    }
}
