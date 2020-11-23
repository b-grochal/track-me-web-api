using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMe.Database.Context;
using TrackMe.Domain.Entities;
using TrackMeWebAPI.Exceptions;
using TrackMeWebAPI.Services.Interfaces;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Logic
{
    public class AdminsService : IAdminsService
    {
        private readonly DatabaseContext databaseContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminsService(DatabaseContext databaseContext, UserManager<ApplicationUser> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }

        public async Task CreateAdmin(NewAdminViewModel newAdminViewModel)
        {
            var duplicatedAdmin = await userManager.FindByEmailAsync(newAdminViewModel.Email);
            if (duplicatedAdmin != null)
            {
                throw new DuplicatedUserException("User with passed email already exists.");
            }

            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = newAdminViewModel.Email,
                UserName = newAdminViewModel.Email
            };

            Admin admin = new Admin
            {
                FirstName = newAdminViewModel.FirstName,
                LastName = newAdminViewModel.LastName,
                Email = newAdminViewModel.Email
            };

            //string hashedPassword = userManager.PasswordHasher.HashPassword(applicationUser, newAdminViewModel.Password);
            //applicationUser.PasswordHash = hashedPassword;
            //userManager.CreateAsync(applicationUser).Wait();
            //userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Admin.ToString()).Wait();
            //admin.ApplicationUserID = applicationUser.Id;
            //admin.Email = applicationUser.Email;
            //databaseContext.Admins.Add(admin as Admin);
            //databaseContext.SaveChanges();
                       
        }

        public async Task DeleteAdmin(int adminId)
        {
            var admin = await databaseContext.Admins.FindAsync(adminId);
            var applicationUser = await userManager.FindByEmailAsync(admin.Email);

            if (admin == null || applicationUser == null)
            {
                throw new UserNotFoundException("Cannot find user with passed ID.");
            }
            databaseContext.Admins.Remove(admin);
            await userManager.DeleteAsync(applicationUser);
        }

        public async Task<AdminViewModel> GetAdminDetails(int adminId)
        {
            var admin = await this.databaseContext.Admins.FindAsync(adminId);

            //if(admin != null)
            //{
            //    return new AdminViewModel
            //    {
            //        ID = admin.ID,
            //        FirstName = admin.FirstName,
            //        LastName = admin.LastName,
            //        Email = admin.Email
            //    };
            //}
            //throw new UserNotFoundException("Cannot find user with passed ID.");
            return null;

        }

        public async Task<IEnumerable<AdminViewModel>> GetAdmins()
        {
            //return await this.databaseContext.Admins
            //    .Select(x => new AdminViewModel
            //    {
            //        ID = x.ID,
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        Email = x.Email
            //    }).ToListAsync();
            return null;
        }
    }
}
