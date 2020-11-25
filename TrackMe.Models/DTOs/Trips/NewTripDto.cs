using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrackMe.Models.DTOs.Trips
{
    public class NewTripDto
    {
        [Required]
        public string Name { get; set; }
    }
}
