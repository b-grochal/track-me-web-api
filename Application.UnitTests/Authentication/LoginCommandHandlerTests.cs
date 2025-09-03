using Application.Authentication.Common;
using Application.Authentication.Login;
using Application.Common.Authentication;
using Application.Common.Data;
using Application.UnitTests.Mocks;
using Common.Results;
using Domain.ApplicationUsers;
using Moq;
using Shouldly;

namespace Application.UnitTests.Authentication;
public class LoginCommandHandlerTests
{
    private static readonly LoginCommand Command = new("test@member.com", "P@ssw0rd123#");

    private readonly LoginCommandHandler _handler;
    private readonly Mock<IApplicationDbContext> _dbContextMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<IJwtProvider> _jwtProviderMock = new();


    public LoginCommandHandlerTests()
    {
        _dbContextMock = ApplicationDbContextMockBuilder.Get();
        _passwordHasherMock = new();
        _jwtProviderMock = new();

        _handler = new LoginCommandHandler(
            _dbContextMock.Object,
            _passwordHasherMock.Object,
            _jwtProviderMock.Object);
    }

    [Fact]
    public async void Handle_Should_ReturnError_WhenUserNotFound()
    {
        // Arrange
        LoginCommand invalidCommand = Command with { Email = "test2@member.com" };

        // Act
        Result result = await _handler.Handle(invalidCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(AuthenticationErrors.ApplicationUserNotFoundByEmail("test2@member.com"));
    }

    [Fact]
    public async void Handle_Should_ReturnError_WhenPasswordIsInvalid()
    {
        // Arrange
        _passwordHasherMock
            .Setup(ph => ph.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        // Act
        Result result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(AuthenticationErrors.ApplicationUserNotFoundByEmail(Command.Email));
    }

    [Fact]
    public async void Handle_Should_ReturnJwtResponse_WhenCredentialsAreValid()
    {
        // Arrange
        _passwordHasherMock
            .Setup(ph => ph.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        _jwtProviderMock
            .Setup(jp => jp.Create(It.IsAny<ApplicationUser>()))
            .Returns("jwt-token");

        // Act
        Result<LoginResponse> result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Jwt.ShouldBe("jwt-token");
    }
}
