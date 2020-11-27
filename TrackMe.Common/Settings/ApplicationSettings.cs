using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMe.Common.Settings
{
    public class ApplicationSettings
    {
        public string ClientURL { get; set; }
        public string AuthSigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
