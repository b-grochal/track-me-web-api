using Common.Errors;

namespace Application.Authentication.Common;

public static class AuthenticationErrors
{
    public static Error ApplicationUserNotFoundByEmail(string email) =>
        Error.NotFound(
            "Authentication.ApplicationUserNotFoundByEmail",
            $"Application user with email '{email}' was not found.");
}
