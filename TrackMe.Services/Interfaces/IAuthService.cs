using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;

namespace TrackMe.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApplicationUser> Authenticate(string email, string password);
        Task Register(BasicUser newBasicUser, string password);
    }
}
