using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.Admins;
using TrackMe.Services.Interfaces;

namespace TrackMe.BusinessServices.Logic
{
    public class AdminsBusinessService : IAdminsBusinessService
    {
        private readonly IMapper mapper;
        private readonly IAdminsService adminsService;

        public AdminsBusinessService(IMapper mapper, IAdminsService adminsService)
        {
            this.mapper = mapper;
            this.adminsService = adminsService;
        }

        public async Task CreateAdmin(NewAdminDto newAdmin)
        {
            var admin = mapper.Map<Admin>(newAdmin);
            await adminsService.CreateAdmin(admin, newAdmin.Password);
        }

        public async Task DeleteAdmin(string adminId)
        {
            await adminsService.DeleteAdmin(adminId);
        }

        public async Task<AdminDto> GetAdmin(string adminId)
        {
            var admin = await adminsService.GetAdmin(adminId);
            return mapper.Map<AdminDto>(admin);
        }

        public async Task<IEnumerable<AdminDto>> GetAdmins()
        {
            var admins = await adminsService.GetAdmins();
            return mapper.Map<IEnumerable<AdminDto>>(admins);
        }
    }
}
