using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;

namespace TrackMe.Domain.Entities
{
    public abstract class ApplicationUser
    {
        public int ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUserIdentity ApplicationUserIdentity { get; set; }
    }
}
