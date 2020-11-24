using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Services.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException(string message) : base(message)
        {
        }
    }
}
