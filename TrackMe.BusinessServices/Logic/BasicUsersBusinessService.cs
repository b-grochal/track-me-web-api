using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Models.DTOs.BasicUsers;
using TrackMe.Services.Interfaces;

namespace TrackMe.BusinessServices.Logic
{
    public class BasicUsersBusinessService : IBasicUsersBusinessService
    {
        private readonly IMapper mapper;
        private readonly IBasicUsersService basicUsersService;

        public BasicUsersBusinessService(IMapper mapper, IBasicUsersService basicUsersService)
        {
            this.mapper = mapper;
            this.basicUsersService = basicUsersService;
        }

        public async Task DeleteBasicUser(string basicUserId)
        {
            await basicUsersService.DeleteBasicUser(basicUserId);
        }

        public async Task<BasicUserDto> GetBasicUser(string basicUserId)
        {
            var basicUser = await basicUsersService.GetBasicUser(basicUserId);
            return mapper.Map<BasicUserDto>(basicUser);
        }

        public async Task<IEnumerable<BasicUserDto>> GetBasicUsers()
        {
            var basicUsers = await basicUsersService.GetBasicUsers();
            return mapper.Map<IEnumerable<BasicUserDto>>(basicUsers);
        }
    }
}
