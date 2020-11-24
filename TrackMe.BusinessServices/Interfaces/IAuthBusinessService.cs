using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Models.DTOs.Auth;

namespace TrackMe.BusinessServices.Interfaces
{
    public interface IAuthBusinessService
    {
        Task<AuthenticatedUserDto> Authenticate(LoginDto loginDto);
        Task Register(RegistrationDto registrationDto);
    }
}
