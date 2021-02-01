using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrackMe.Models.DTOs.Trips
{
    public class NewSensorDataDto
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double AccelerometerX { get; set; }

        [Required]
        public double AccelerometerY { get; set; }

        [Required]
        public double AccelerometerZ { get; set; }

        [Required]
        public double GyroscopeX { get; set; }

        [Required]
        public double GyroscopeY { get; set; }

        [Required]
        public double GyroscopeZ { get; set; }

        [Required]
        public double MagneticFieldX { get; set; }

        [Required]
        public double MagneticFieldY { get; set; }

        [Required]
        public double MagneticFieldZ { get; set; }
    }
}
