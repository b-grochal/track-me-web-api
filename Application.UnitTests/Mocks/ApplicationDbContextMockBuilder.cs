using Application.Common.Data;
using Domain.Admins;
using Domain.ApplicationUsers;
using Domain.Members;
using MockQueryable.Moq;
using Moq;

namespace Application.UnitTests.Mocks;
public static class ApplicationDbContextMockBuilder
{
    public static Mock<IApplicationDbContext> Get()
    {
        var applicationsUsers = new List<ApplicationUser>
        {
            new Admin
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Admin",
                Email = "test@admin.com"
            },
            new Member
            {
                Id = 2,
                FirstName = "Test",
                LastName = "Member",
                Email = "test@member.com"
            }
        };

        var applicationUsersDbSetMock = applicationsUsers.BuildMockDbSet();

        var applicationDbContextMock = new Mock<IApplicationDbContext>();
        applicationDbContextMock.Setup(db => db.ApplicationUsers).Returns(applicationUsersDbSetMock.Object);

        return applicationDbContextMock;
    }
}
