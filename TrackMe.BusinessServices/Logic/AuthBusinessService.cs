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
using TrackMe.Models.DTOs.Auth;
using TrackMe.Services.Interfaces;

namespace TrackMe.BusinessServices.Logic
{
    public class AuthBusinessService : IAuthBusinessService
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly IApplicationUserRolesService applicationUserRolesService;
        private readonly string authSigningKeyValue = "123456789012345678901234567890";
        private readonly string issuer = "TrackMeIssuer";
        private readonly string audience = "TrackMeAudience";

        public AuthBusinessService(IMapper mapper, IAuthService authService, IApplicationUserRolesService applicationUserRolesService)
        {
            this.mapper = mapper;
            this.authService = authService;
            this.applicationUserRolesService = applicationUserRolesService;
        }

        public async Task<AuthenticatedUserDto> Authenticate(LoginDto loginDto)
        {
            var authenticatedBasicUser = await authService.Authenticate(loginDto.Email, loginDto.Password);
            var role = await applicationUserRolesService.GetApplicationUserRole(authenticatedBasicUser.Id);

            return new AuthenticatedUserDto
            {
                Token = GenerateToken(authenticatedBasicUser.Id, authenticatedBasicUser.Email, role),
                Role = role
            };
        }

        public async Task Register(RegistrationDto registrationDto)
        {
            var newBasicUser = mapper.Map<BasicUser>(registrationDto);
            await authService.Register(newBasicUser, registrationDto.Password);
        }

        private string GenerateToken(string applicationUserId, string email, string role)
        {
            var authClaims = new[]
                {
                    new Claim("ApplicationUserId", applicationUserId),
                    new Claim("Email", email),
                    new Claim("Role", role),
                    new Claim(ClaimTypes.Role, role)
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSigningKeyValue));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
