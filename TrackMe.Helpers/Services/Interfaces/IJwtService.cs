using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMe.Helpers.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string applicationUserId, string email, string role);
    }
}
