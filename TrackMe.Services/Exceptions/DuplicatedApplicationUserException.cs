using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMe.Services.Exceptions
{
    public class DuplicatedApplicationUserException : Exception
    {
        public DuplicatedApplicationUserException(string message)
            : base(message)
        {
        }
    }
}
