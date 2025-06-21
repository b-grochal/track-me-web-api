using Application.Common.Messaging;

namespace Application.Common.Authentication.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<string>;
