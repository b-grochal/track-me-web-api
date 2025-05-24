using Application.Common.Data;
using Domain.Admins;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using TrackMe.Database.Context;

namespace Infrastructure.Admins;

internal class AdminsRepository : Repository<Admin>, IAdminsRepository
{
    public AdminsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Admin?> GetByEmailAsync(string email)
    {
        return await _dbContext.Admins.FirstOrDefaultAsync(x => x.Email == email);
    }
}
