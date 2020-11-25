using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.Trips;

namespace TrackMe.Models.Profiles
{
    public class TripsProfile : Profile
    {
        public TripsProfile()
        {
            CreateMap<NewTripDto, Trip>();

            CreateMap<Trip, TripDto>()
                .ForMember(dest => dest.BasicUserEmail, opts => opts.MapFrom(src => src.BassicUser.Email));

            CreateMap<NewSensorDataDto, SensorData>()
                .ForMember(dest => dest.UploadDate, opts => opts.MapFrom(d => DateTime.Now));

            CreateMap<SensorData, SensorDataDto>()
                .ForMember(dest => dest.UploadDate, opts => opts.MapFrom(src => src.UploadDate.ToString()));

            CreateMap<Trip, TripSensorDataDto>()
                .ForMember(dest => dest.BasicUserEmail, opts => opts.MapFrom(src => src.BassicUser.Email))
                .ForMember(dest => dest.SensorData, opts => opts.MapFrom(src => src.SensorValues));
        }
    }
}
