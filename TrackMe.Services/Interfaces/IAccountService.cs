using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.ApplicationUsers;

namespace TrackMe.Services.Interfaces
{
    public interface IAccountService
    {
        Task ChangePassword(string applicationUserId, string oldPassword, string newPassword);
        Task UdpateAccountData(ApplicationUser applicationUser);
        Task<ApplicationUser> GetApplicationUser(string applicationUserId);
    }
}
