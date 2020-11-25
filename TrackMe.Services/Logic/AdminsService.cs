using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Database.Context;
using TrackMe.Domain.Entities;
using TrackMe.Services.Exceptions;
using TrackMe.Services.Interfaces;

namespace TrackMe.Services.Logic
{
    public class AdminsService : IAdminsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DatabaseContext databaseContext;

        public AdminsService(UserManager<ApplicationUser> userManager, DatabaseContext databaseContext)
        {
            this.userManager = userManager;
            this.databaseContext = databaseContext;
        }

        public async Task CreateAdmin(Admin newAdmin, string password)
        {
            var isAdminDuplicated = await userManager.FindByEmailAsync(newAdmin.Email) == null;

            if (isAdminDuplicated)
            {
                throw new DuplicatedApplicationUserException("User with passed email already exists.");
            }

            await userManager.CreateAsync(newAdmin, password);
            await userManager.AddToRoleAsync(newAdmin, ApplicationUserRoles.Admin);
        }

        public async Task DeleteAdmin(string adminId)
        {
            var admin = await userManager.FindByIdAsync(adminId);
            await userManager.DeleteAsync(admin);
        }

        public async Task<Admin> GetAdmin(string adminId)
        {
            return await databaseContext.Admins
                .FindAsync(adminId);
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await databaseContext.Admins
                .ToListAsync();
        }
    }
}
