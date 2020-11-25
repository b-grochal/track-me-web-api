using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Models.DTOs.BasicUsers
{
    public class BasicUserDto
    {
        public int BasicUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
