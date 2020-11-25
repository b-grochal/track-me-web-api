using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;

namespace TrackMe.Services.Interfaces
{
    public interface IAdminsService
    {
        Task<IEnumerable<Admin>> GetAdmins();

        Task<Admin> GetAdmin(string adminId);

        Task CreateAdmin(Admin newAdmin, string password);

        Task DeleteAdmin(string adminId);
    }
}
