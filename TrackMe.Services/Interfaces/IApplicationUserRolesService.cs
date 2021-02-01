using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrackMe.Services.Interfaces
{
    public interface IApplicationUserRolesService
    {
        Task<string> GetApplicationUserRole(string applicationUserId);
    }
}
