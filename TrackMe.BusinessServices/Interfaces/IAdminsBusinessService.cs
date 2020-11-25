using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Models.DTOs.Admins;

namespace TrackMe.BusinessServices.Interfaces
{
    public interface IAdminsBusinessService
    {
        Task<IEnumerable<AdminDto>> GetAdmins();

        Task<AdminDto> GetAdmin(string adminId);

        Task CreateAdmin(NewAdminDto newAdmin);

        Task DeleteAdmin(string adminId);
    }
}
