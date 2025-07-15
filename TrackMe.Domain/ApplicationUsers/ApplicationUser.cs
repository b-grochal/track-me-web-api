using Domain.Common;

namespace Domain.ApplicationUsers;

public abstract class ApplicationUser : Entity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public ApplicationUserRole Role { get; set; }
}
