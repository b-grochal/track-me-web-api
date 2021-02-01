using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.Account;

namespace TrackMe.Models.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountDataDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, opts => opts.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedEmail, opts => opts.MapFrom(src => src.Email.ToUpper()));

            CreateMap<ApplicationUser, AccountDataDto>();
        }
    }
}
