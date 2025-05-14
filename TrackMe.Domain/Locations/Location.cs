using Domain.Trips;
using NetTopologySuite.Geometries;
using System;

namespace Domain.Locations;

public class Location
{
    public int Id { get; set; }

    public DateTimeOffset CapturedDate { get; set; }

    public Point Position { get; set; }

    public int TripId { get; set; }

    public virtual Trip Trip { get; set; }
}
