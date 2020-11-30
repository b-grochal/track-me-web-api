using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Models.DTOs.Account;

namespace TrackMeWebAPI.Controllers
{
    [Authorize(Roles = "BasicUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBusinessService accountBusinessService;

        public AccountController(IAccountBusinessService accountBusinessService)
        {
            this.accountBusinessService = accountBusinessService;
        }

        // POST: api/account/change-password
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var applicationUserId = User.Claims.First(x => x.Type == "ApplicationUserId").Value;
            await accountBusinessService.ChangePassword(applicationUserId, changePasswordDto);
            return Ok();
        }

        // POST: api/account/account-data
        [HttpPost]
        [Route("account-data")]
        public async Task<IActionResult> UpdateAccountData([FromBody] AccountDataDto updateAccountDataDto)
        {
            var applicationUserId = User.Claims.First(x => x.Type == "ApplicationUserId").Value;
            await accountBusinessService.UpdateAccountData(applicationUserId, updateAccountDataDto);
            return Ok();
        }

        // GET: api/account/account-data
        [HttpGet]
        [Route("account-data")]
        public async Task<IActionResult> GetAccountData([FromBody] AccountDataDto updateAccountDataDto)
        {
            var applicationUserId = User.Claims.First(x => x.Type == "ApplicationUserId").Value;
            var accountData = await accountBusinessService.GetAccountData(applicationUserId);
            return Ok(accountData);
        }

    }
}