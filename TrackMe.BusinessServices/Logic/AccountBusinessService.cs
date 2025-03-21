﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.Account;
using TrackMe.Services.Interfaces;

namespace TrackMe.BusinessServices.Logic
{
    public class AccountBusinessService : IAccountBusinessService
    {
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        
        public AccountBusinessService(IMapper mapper, IAccountService accountService)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        public async Task ChangePassword(string applicationUserId, ChangePasswordDto changePasswordDto)
        {
            await accountService.ChangePassword(applicationUserId, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        }

        public async Task UpdateAccountData(string applicationUserId, AccountDataDto updateAccountDataDto)
        {
            var applicationUser = await accountService.GetApplicationUser(applicationUserId);
            var updatedApplicationUser = mapper.Map(updateAccountDataDto, applicationUser);
            await accountService.UdpateAccountData(updatedApplicationUser);
        }

        public async Task<AccountDataDto> GetAccountData(string applicationUserId)
        {
            var applicationUser = await accountService.GetApplicationUser(applicationUserId);
            return mapper.Map<AccountDataDto>(applicationUser);
        }
    }
}
