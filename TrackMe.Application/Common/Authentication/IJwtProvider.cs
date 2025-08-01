using Domain.ApplicationUsers;

namespace Application.Common.Authentication;

public interface IJwtProvider
{
    string Create(ApplicationUser applicationUser);
}
