using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Models.DTOs.BasicUsers;

namespace TrackMe.BusinessServices.Interfaces
{
    public interface IBasicUsersBusinessService
    {
        Task<IEnumerable<BasicUserDto>> GetBasicUsers();
        Task<BasicUserDto> GetBasicUser(string basicUserId);
        Task DeleteBasicUser(string basicUserId);
    }
}
