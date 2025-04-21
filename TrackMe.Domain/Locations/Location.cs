using System;
using Domain.Trips;

namespace Domain.Locations
{
    public class Location
    {
        public int LocationId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTimeOffset UploadTime { get; set; }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
