using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.BusinessServices.Logic;
using TrackMe.Common.Settings;
using TrackMe.Database.Context;
using TrackMe.Domain.Entities;
using TrackMe.Helpers.Services.Interfaces;
using TrackMe.Helpers.Services.Logic;
using TrackMe.Models.Profiles;
using TrackMe.Services.Interfaces;
using TrackMe.Services.Logic;
using TrackMe.WebApi.Middlewares;
using TrackMeWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// == Services ==
builder.Services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseLazyLoadingProxies()
           .UseSqlServer(configuration["ConnectionString:DBConnection"], x =>
               x.MigrationsAssembly("TrackMe.Database")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

// == Swagger ==
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackMe WebAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }});
});

// == JWT ==
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["ApplicationSettings:Audience"],
        ValidIssuer = configuration["ApplicationSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationSettings:AuthSigningKey"]))
    };
});

builder.Services.AddCors();

// == DI: Services ==
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAdminsService, AdminsService>();
builder.Services.AddTransient<IApplicationUserRolesService, ApplicationUserRolesService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IBasicUsersService, BasicUsersService>();
builder.Services.AddTransient<ITripsService, TripsService>();

builder.Services.AddTransient<IAccountBusinessService, AccountBusinessService>();
builder.Services.AddTransient<IAdminsBusinessService, AdminsBusinessService>();
builder.Services.AddTransient<IAuthBusinessService, AuthBusinessService>();
builder.Services.AddTransient<IBasicUsersBusinessService, BasicUsersBusinessService>();
builder.Services.AddTransient<ITripsBusinessService, TripsBusinessService>();

builder.Services.AddTransient<IJwtService, JwtService>();

// == AutoMapper ==
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AuthProfile());
    mc.AddProfile(new AccountProfile());
    mc.AddProfile(new AdminsProfile());
    mc.AddProfile(new BasicUsersProfile());
    mc.AddProfile(new TripsProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

var app = builder.Build();

// == DB Seeder ==
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DatabaseContext>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        DatabaseSeeder.SeedData(context, roleManager, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// == Middleware ==
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackMe WebAPI");
});

app.MapControllers();

app.Run();