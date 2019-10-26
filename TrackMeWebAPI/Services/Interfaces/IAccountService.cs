using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoggedUserViewModel> Login(LoginViewModel loginViewModel);

        Task<bool> Register(RegisterViewModel registerViewModel);
    }
}
