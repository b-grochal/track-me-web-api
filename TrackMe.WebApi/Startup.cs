using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

namespace TrackMeWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<DatabaseContext>(op => op.UseLazyLoadingProxies().UseSqlServer(Configuration["ConnectionString:DBConnection"], x => x.MigrationsAssembly("TrackMe.Database")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackMe WebAPI", Version = "v1" });
            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["ApplicationSettings:Audience"],
                    ValidIssuer = Configuration["ApplicationSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:AuthSigningKey"]))
                };
            });

            services.AddCors();
            
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAdminsService, AdminsService>();
            services.AddTransient<IApplicationUserRolesService, ApplicationUserRolesService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IBasicUsersService, BasicUsersService>();
            services.AddTransient<ITripsService, TripsService>();

            services.AddTransient<IAccountBusinessService, AccountBusinessService>();
            services.AddTransient<IAdminsBusinessService, AdminsBusinessService>();
            services.AddTransient<IAuthBusinessService, AuthBusinessService>();
            services.AddTransient<IBasicUsersBusinessService, BasicUsersBusinessService>();
            services.AddTransient<ITripsBusinessService, TripsBusinessService>();

            services.AddTransient<IJwtService, JwtService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AuthProfile());
                mc.AddProfile(new AccountProfile());
                mc.AddProfile(new AdminsProfile());
                mc.AddProfile(new BasicUsersProfile());
                mc.AddProfile(new TripsProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackMe WebAPI");
            });

        }
    }
}
