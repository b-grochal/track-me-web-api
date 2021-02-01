using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Database.Context;
using TrackMe.Domain.Entities;
using TrackMe.Services.Interfaces;

namespace TrackMe.Services.Logic
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

        public async Task DeleteBasicUser(string basicUserId)
        {
            var basicUser = await userManager.FindByIdAsync(basicUserId);
            await userManager.DeleteAsync(basicUser);
        }

        public async Task<BasicUser> GetBasicUser(string basicUserId)
        {
            return await databaseContext.BasicUsers
                .FindAsync(basicUserId);
        }

        public async Task<IEnumerable<BasicUser>> GetBasicUsers()
        {
            return await databaseContext.BasicUsers
                .ToListAsync();
        }
    }
}
