using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMeWebAPI.Exceptions
{
    public class DuplicatedUserException : Exception
    {
        public DuplicatedUserException()
        {
        }

        public DuplicatedUserException(string message)
            : base(message)
        {
        }
    }
}
