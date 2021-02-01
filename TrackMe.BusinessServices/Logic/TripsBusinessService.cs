using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Domain.Entities;
using TrackMe.Models.DTOs.Trips;
using TrackMe.Services.Interfaces;

namespace TrackMe.BusinessServices.Logic
{
    public class TripsBusinessService : ITripsBusinessService
    {
        private readonly IMapper mapper;
        private readonly ITripsService tripsService;

        public TripsBusinessService(IMapper mapper, ITripsService tripsService)
        {
            this.mapper = mapper;
            this.tripsService = tripsService;
        }

        public async Task CreateTrip(string basicUserId, NewTripDto newTrip)
        {
            var trip = mapper.Map<Trip>(newTrip);
            trip.BasicUserId = basicUserId;
            await tripsService.CreateTrip(trip);
        }

        public async Task CreateTripSensorData(int tripId, NewSensorDataDto newSensorData)
        {
            var sensorData = mapper.Map<SensorData>(newSensorData);
            sensorData.TripId = tripId;
            await tripsService.CreateTripSensorData(sensorData);
        }

        public async Task DeleteTrip(int tripId)
        {
            await tripsService.DeleteTrip(tripId);
        }

        public async Task<TripDto> GetTrip(int tripId)
        {
            var trip = await tripsService.GetTrip(tripId);
            return mapper.Map<TripDto>(trip);
        }

        public async Task<IEnumerable<TripDto>> GetTrips()
        {
            var trips = await tripsService.GetTrips();
            return mapper.Map<IEnumerable<TripDto>>(trips);
        }

        public async Task<IEnumerable<TripDto>> GetTrips(string basicUserId)
        {
            var trips = await tripsService.GetTrips(basicUserId);
            return mapper.Map<IEnumerable<TripDto>>(trips);
        }

        public async Task<TripSensorDataDto> GetTripSensorDataDto(int tripId)
        {
            var trip = await tripsService.GetTrip(tripId);
            return mapper.Map<TripSensorDataDto>(trip);
        }
    }
}
