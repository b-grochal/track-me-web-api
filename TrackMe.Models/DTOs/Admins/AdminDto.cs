using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Models.DTOs.Admins
{
    public class AdminDto
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
