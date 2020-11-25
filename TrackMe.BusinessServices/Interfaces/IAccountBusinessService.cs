using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Models.DTOs.Account;

namespace TrackMe.BusinessServices.Interfaces
{
    public interface IAccountBusinessService
    {
        Task ChangePassword(string applicationUserId, ChangePasswordDto changePasswordDto);
        Task UpdateAccountData(string applicationUserId, UpdateAccountDataDto updateAccountDataDto);
    }
}
