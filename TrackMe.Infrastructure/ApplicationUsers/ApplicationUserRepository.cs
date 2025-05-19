using Application.Common.Data;
using Domain.ApplicationUsers;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using TrackMe.Database.Context;

namespace Infrastructure.ApplicationUsers
{
    internal class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
