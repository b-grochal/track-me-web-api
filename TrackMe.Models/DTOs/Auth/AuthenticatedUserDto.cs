using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Models.DTOs.Auth
{
    public class AuthenticatedUserDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
