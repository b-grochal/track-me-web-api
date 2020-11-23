using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMe.Services.Exceptions
{
    public class ApplicationUserNotFoundException : Exception
    {
        public ApplicationUserNotFoundException(string message)
            : base(message)
        {
        }
    }
}
