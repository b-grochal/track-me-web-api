using Application.Common.Data;
using Domain.Members;
using Infrastructure.Common;
using TrackMe.Database.Context;

namespace Infrastructure.Members;

internal class MembersRepository : Repository<Member>, IMembersRepository
{
    public MembersRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
