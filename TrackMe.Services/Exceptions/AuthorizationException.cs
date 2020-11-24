using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Services.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message)
        {
        }
    }
}
