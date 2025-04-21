using AutoMapper;
using Domain.Members;
using System;
using System.Collections.Generic;
using System.Text;
using TrackMe.Models.DTOs.BasicUsers;

namespace TrackMe.Models.Profiles
{
    public class BasicUsersProfile : Profile
    {
        public BasicUsersProfile()
        {
            CreateMap<Member, BasicUserDto>()
                .ForMember(dest => dest.BasicUserId, opts => opts.MapFrom(src => src.Id));
        }
    }
}
