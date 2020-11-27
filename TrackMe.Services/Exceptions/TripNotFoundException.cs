using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMe.Services.Exceptions
{
    public class TripNotFoundException : Exception
    {
        public TripNotFoundException(string message) : base(message)
        {
        }
    }
}
