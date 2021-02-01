using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.BasicUsers;

namespace TrackMe.Models.Profiles
{
    public class BasicUsersProfile : Profile
    {
        public BasicUsersProfile()
        {
            CreateMap<BasicUser, BasicUserDto>()
                .ForMember(dest => dest.BasicUserId, opts => opts.MapFrom(src => src.Id));
        }
    }
}
