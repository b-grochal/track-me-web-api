using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackMe.Common.Settings;
using TrackMe.Helpers.Services.Interfaces;

namespace TrackMe.Helpers.Services.Logic
{
    public class JwtService : IJwtService
    {
        private readonly ApplicationSettings applicationSettings;

        public JwtService(IOptions<ApplicationSettings> applicationSettings)
        {
            this.applicationSettings = applicationSettings.Value;
        }

        public string GenerateToken(string applicationUserId, string email, string role)
        {
            var authClaims = new[]
                {
                    new Claim("ApplicationUserId", applicationUserId),
                    new Claim("Email", email),
                    new Claim("Role", role),
                    new Claim(ClaimTypes.Role, role)
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSettings.AuthSigningKey));

            var token = new JwtSecurityToken(
                issuer: applicationSettings.Issuer,
                audience: applicationSettings.Audience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
