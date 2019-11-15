using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMeWebAPI.Exceptions
{
    public class TripNotFoundException : Exception
    {
        public TripNotFoundException()
        {
        }

        public TripNotFoundException(string message)
            : base(message)
        {
        }
    }
}
