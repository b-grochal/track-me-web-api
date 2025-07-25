using Application.Common.Messaging;

namespace Application.Authentication.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<string>;
