using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.Admins;

namespace TrackMe.Models.Profiles
{
    public class AdminsProfile : Profile
    {
        public AdminsProfile()
        {
            CreateMap<Admin, AdminDto>()
                .ForMember(dest => dest.AdminId, opts => opts.MapFrom(src => src.Id));

            CreateMap<NewAdminDto, Admin>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, opts => opts.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedEmail, opts => opts.MapFrom(src => src.Email.ToUpper()));
        }
    }
}
