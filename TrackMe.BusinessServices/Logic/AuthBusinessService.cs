using AutoMapper;
using System;
using System.Collections.Generic;
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
        

        public AuthBusinessService(IMapper mapper, IAuthService authService)
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        public async Task<AuthenticatedUserDto> Authenticate(string email, string password)
        {
            var authenticatedBasicUser = await authService.Authenticate(email, password);
            return mapper.Map<AuthenticatedUserDto>(authenticatedBasicUser);
        }

        public async Task Register(RegistrationDto registrationDto)
        {
            var newBasicUser = mapper.Map<BasicUser>(registrationDto);
            await authService.Register(newBasicUser, registrationDto.Password);
        }
    }
}
