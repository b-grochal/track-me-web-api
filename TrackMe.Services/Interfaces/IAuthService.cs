using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.ApplicationUsers;
using Domain.Members;

namespace TrackMe.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApplicationUser> Authenticate(string email, string password);
        Task Register(Member newBasicUser, string password);
    }
}
