using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Interfaces
{
    public interface IBasicUsersService
    {
        Task<IEnumerable<BasicUserViewModel>> GetAllBasicUsers();

        Task<BasicUserViewModel> GetBasicUserDetails(int basicUserId);

        Task<BasicUserViewModel> GetBasicUserAccountDetails(string applicationUserId);

        Task DeleteBasicUser(int basicUserId);

        Task UpdateBasicUser(UpdatedBasicUserViewModel updatedBasicUser);



    }
}
