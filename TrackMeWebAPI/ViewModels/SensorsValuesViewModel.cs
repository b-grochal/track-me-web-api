using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMeWebAPI.ViewModels
{
    public class SensorsValuesViewModel
    {
        public string UploadDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AccelerometerX { get; set; }
        public double AccelerometerY { get; set; }
        public double AccelerometerZ { get; set; }
        public double GyroscopeX { get; set; }
        public double GyroscopeY { get; set; }
        public double GyroscopeZ { get; set; }
        public double MagneticFieldX { get; set; }
        public double MagneticFieldY { get; set; }
        public double MagneticFieldZ { get; set; }
    }
}
