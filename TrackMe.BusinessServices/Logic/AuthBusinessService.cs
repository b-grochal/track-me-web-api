using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Domain.Entities;
using TrackMe.Helpers.Services.Interfaces;
using TrackMe.Models.DTOs.Auth;
using TrackMe.Services.Interfaces;

namespace TrackMe.BusinessServices.Logic
{
    public class AuthBusinessService : IAuthBusinessService
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly IApplicationUserRolesService applicationUserRolesService;
        private readonly IJwtService jwtService;

        public AuthBusinessService(IMapper mapper, IAuthService authService, IApplicationUserRolesService applicationUserRolesService, IJwtService jwtService)
        {
            this.mapper = mapper;
            this.authService = authService;
            this.applicationUserRolesService = applicationUserRolesService;
            this.jwtService = jwtService;
        }

        public async Task<AuthenticatedUserDto> Authenticate(LoginDto loginDto)
        {
            var authenticatedBasicUser = await authService.Authenticate(loginDto.Email, loginDto.Password);
            var role = await applicationUserRolesService.GetApplicationUserRole(authenticatedBasicUser.Id);

            return new AuthenticatedUserDto
            {
                Token = jwtService.GenerateToken(authenticatedBasicUser.Id, authenticatedBasicUser.Email, role),
                Role = role
            };
        }

        public async Task Register(RegistrationDto registrationDto)
        {
            var newBasicUser = mapper.Map<BasicUser>(registrationDto);
            await authService.Register(newBasicUser, registrationDto.Password);
        }
    }
}
