using Application.Authentication.Login;
using Application.Common.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services) =>
        services
            .AddCommands();

    // TODO: Prepare automatic registration of commands
    public static IServiceCollection AddCommands(
        this IServiceCollection services) 
    {
        services.AddScoped<ICommandHandler<LoginCommand, string>, LoginCommandHandler>();

        return services;
    }
}
