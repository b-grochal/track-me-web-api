using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Exceptions;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.Services.Interfaces;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Logic
{
    public class BasicUsersService : IBasicUsersService
    {
        private readonly DatabaseContext databaseContext;
        private readonly UserManager<ApplicationUser> userManager;

        public BasicUsersService(DatabaseContext databaseContext, UserManager<ApplicationUser> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }

        public async Task DeleteBasicUser(int basicUserId)
        {
            var basicUser = await databaseContext.BasicUsers.FindAsync(basicUserId);
            var applicationUser = await userManager.FindByEmailAsync(basicUser.Email);

            if (basicUser != null && applicationUser != null)
            {
                databaseContext.BasicUsers.Remove(basicUser);
                await userManager.DeleteAsync(applicationUser);
                databaseContext.SaveChanges();
            }
            throw new UserNotFoundException("Cannot find user with passed ID.");
        }

        public async Task<IEnumerable<BasicUserViewModel>> GetAllBasicUsers()
        {
            return await this.databaseContext.BasicUsers
                .Select(x => new BasicUserViewModel
                {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                }).ToListAsync();
        }

        public async Task<BasicUserViewModel> GetBasicUserAccountDetails(string applicationUserId)
        {
            var basicUser = await this.databaseContext.BasicUsers.SingleOrDefaultAsync(x => x.ApplicationUserID.Equals(applicationUserId));

            if(basicUser != null)
            {
                return new BasicUserViewModel
                {
                    ID = basicUser.ID,
                    FirstName = basicUser.FirstName,
                    LastName = basicUser.LastName,
                    Email = basicUser.Email,
                    PhoneNumber = basicUser.PhoneNumber
                };
            }
            throw new UserNotFoundException("Cannot find user with passed ID.");
        }

        public async Task<BasicUserViewModel> GetBasicUserDetails(int basicUserId)
        {
            var basicUser = await this.databaseContext.BasicUsers.FindAsync(basicUserId);

            if(basicUser != null)
            {
                return new BasicUserViewModel
                {
                    ID = basicUser.ID,
                    FirstName = basicUser.FirstName,
                    LastName = basicUser.LastName,
                    Email = basicUser.Email,
                    PhoneNumber = basicUser.PhoneNumber
                };
            }
            throw new UserNotFoundException("Cannot find user with passed ID.");
            
        }

        public async Task UpdateBasicUser(UpdatedBasicUserViewModel updatedBasicUser)
        {
            var oldBasicUser = await databaseContext.BasicUsers.FindAsync(updatedBasicUser.ID);
            var applicationUser = await userManager.FindByEmailAsync(oldBasicUser.Email);
            if(oldBasicUser == null || applicationUser == null)
            {
                throw new UserNotFoundException("Cannot find user with passed ID.");
            }
            applicationUser.Email = updatedBasicUser.Email;
            applicationUser.UserName = updatedBasicUser.Email;
            oldBasicUser.Email = updatedBasicUser.Email;
            oldBasicUser.FirstName = updatedBasicUser.FirstName;
            oldBasicUser.LastName = updatedBasicUser.LastName;
            oldBasicUser.PhoneNumber = updatedBasicUser.PhoneNumber;
            await userManager.UpdateAsync(applicationUser);
            databaseContext.BasicUsers.Update(oldBasicUser);
            databaseContext.SaveChanges();
                   
        }
    }
}
