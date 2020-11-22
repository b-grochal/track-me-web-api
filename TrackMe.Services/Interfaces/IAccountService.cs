using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;
using TrackMeWebAPI.Models;

namespace TrackMe.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserIdentity> Authenticate(string email, string password);
    }
}
