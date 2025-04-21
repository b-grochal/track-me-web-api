using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Members;

namespace TrackMe.Services.Interfaces
{
    public interface IBasicUsersService
    {
        Task<IEnumerable<Member>> GetBasicUsers();
        Task<Member> GetBasicUser(string basicUserId);
        Task DeleteBasicUser(string basicUserId);
    }
}
