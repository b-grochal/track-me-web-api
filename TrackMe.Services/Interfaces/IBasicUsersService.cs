using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;

namespace TrackMe.Services.Interfaces
{
    public interface IBasicUsersService
    {
        Task<IEnumerable<BasicUser>> GetBasicUsers();

        Task<BasicUser> GetBasicUser(string basicUserId);

        Task DeleteBasicUser(string basicUserId);
    }
}
