using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Interfaces
{
    public interface IAdminsService
    {
        Task<IEnumerable<AdminViewModel>> GetAdmins();

        Task<AdminViewModel> GetAdminDetails(int adminId);

        Task CreateAdmin(NewAdminViewModel newAdminViewModel);

        Task DeleteAdmin(int adminId);
    }
}
