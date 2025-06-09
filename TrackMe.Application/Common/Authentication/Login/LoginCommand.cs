using Application.Common.Messaging;

namespace Application.Common.Authentication.Login;

public sealed record LoginCommand(string email, string password) : ICommand<object>;
