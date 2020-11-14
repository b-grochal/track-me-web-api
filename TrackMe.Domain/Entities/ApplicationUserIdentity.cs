using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TrackMeWebAPI.Models;

namespace TrackMe.Domain.Entities
{
    public class ApplicationUserIdentity : IdentityUser
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
