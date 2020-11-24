using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.Auth;

namespace TrackMe.Models.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegistrationDto, BasicUser>();
        }
    }
}
