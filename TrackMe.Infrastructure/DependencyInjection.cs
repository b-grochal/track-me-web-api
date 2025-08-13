using Application.Common.Authentication;
using Application.Common.Data;
using Domain.Admins;
using Domain.ApplicationUsers;
using Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackMe.Database.Context;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddAuthentication(configuration);

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>((serviceProvide, options) =>
        {
            var passwordHasher = serviceProvide.GetRequiredService<IPasswordHasher>();

            var admin = new Admin
            {
                FirstName = "Super",
                LastName = "Admin",
                Email = "super@admin.com",
                PasswordHash = passwordHasher.Hash("P@ssw0rd123#"),
                Role = ApplicationUserRole.Admin
            };

            options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.UseNetTopologySuite())
                .UseSeeding((context, _) => 
                {
                    var seededAdmin = context.Set<Admin>().FirstOrDefault(a => a.Email == admin.Email);

                    if (seededAdmin is null)
                    {
                        context.Set<Admin>().Add(admin);
                        context.SaveChanges();
                    }
                })
                .UseAsyncSeeding(async (context, _, CancellationToken) =>
                {
                    var seededAdmin = await context.Set<Admin>().FirstOrDefaultAsync(a => a.Email == admin.Email);

                    if (seededAdmin is null)
                    {
                        await context.Set<Admin>().AddAsync(admin, CancellationToken);
                        await context.SaveChangesAsync(CancellationToken);
                    }
                });
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtProvider, JwtProvider>();

        return services;
    }
}
