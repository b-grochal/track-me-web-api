using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Models;

namespace TrackMeWebAPI.Data
{
    public class DataSeeder
    {
        public static void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.Roles.Any())
            {
                var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
                roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin.ToString())).Wait();
                roleManager.CreateAsync(new IdentityRole(ApplicationRoles.BasicUser.ToString())).Wait();
                dbContext.SaveChanges();
            }
        }

        public static void CreateAdmins(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            if(!dbContext.Admins.Any())
            {
                ApplicationUser applicationAdmin = new ApplicationUser
                {
                    Email = "adam@gmail.com",
                    UserName = "adam@gmail.com"
                };

                string hashedPassword = userManager.PasswordHasher.HashPassword(applicationAdmin, "P@ssw0rd");
                applicationAdmin.PasswordHash = hashedPassword;
                userManager.CreateAsync(applicationAdmin).Wait();
                userManager.AddToRoleAsync(applicationAdmin, ApplicationRoles.Admin.ToString()).Wait();

                var admin = new Admin
                {
                    FirstName = "Adam",
                    LastName = "Cole",
                    ApplicationUserID = applicationAdmin.Id,
                    Email = applicationAdmin.Email

                };

                dbContext.Admins.Add(admin as Admin);
                dbContext.SaveChanges();


            }


        }

        public static void CreateBasicUsers(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.BasicUsers.Any())
            {
                List<ApplicationUser> applicationUsers = new List<ApplicationUser>
                {
                    new ApplicationUser {Email = "joe@gmail.com", UserName = "joe@gmail.com"},
                    new ApplicationUser {Email = "kyle@gmail.com", UserName = "kyle@gmail.com"}
                };

                List<BasicUser> basicUsers = new List<BasicUser>
                {
                    new BasicUser {FirstName = "Joe", LastName = "Page", PhoneNumber="666-777-888"},
                    new BasicUser {FirstName = "Kyle", LastName = "Walker", PhoneNumber="111-222-333"}
                };

                for(int i = 0; i < applicationUsers.Count; i++)
                {
                    string hashedPassword = userManager.PasswordHasher.HashPassword(applicationUsers[i], "P@ssw0rd");
                    applicationUsers[i].PasswordHash = hashedPassword;
                    userManager.CreateAsync(applicationUsers[i]).Wait();
                    userManager.AddToRoleAsync(applicationUsers[i], ApplicationRoles.BasicUser.ToString()).Wait();

                    basicUsers[i].ApplicationUserID = applicationUsers[i].Id;
                    basicUsers[i].Email = applicationUsers[i].Email;
                    
                    dbContext.BasicUsers.Add(basicUsers[i] as BasicUser);
                }

                dbContext.SaveChanges();


            }
        }

        public static void CreateTrips(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Trips.Any())
            {
                List<Trip> trips = new List<Trip>
                {
                    new Trip{Name="Holiday Trip", BasicUserID = dbContext.BasicUsers.Single(x => x.ID == 1).ID},
                    new Trip{Name="Sunday Trip", BasicUserID = dbContext.BasicUsers.Single(x => x.ID == 2).ID},
                    new Trip{Name="Moscow Trip", BasicUserID = dbContext.BasicUsers.Single(x => x.ID == 2).ID}
                };
                           foreach(Trip trip in trips)
                {
                    dbContext.Trips.Add(trip as Trip);
                }

                dbContext.SaveChanges();


            }
        }

        public static void CreateSensorsValues(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.SensorValues.Any())
            {
                List<SensorValues> sensorValues = new List<SensorValues>
                {
                    new SensorValues{Latitude = 50, Longitude = 50, TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorValues{Latitude = 50.2, Longitude = 50, TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorValues{Latitude = 50, Longitude = 50.2, TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorValues{Latitude = 50, Longitude = 50.3, TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorValues{Latitude = 20, Longitude = 20, TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorValues{Latitude = 20.2, Longitude = 20, TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorValues{Latitude = 20.3, Longitude = 20, TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorValues{Latitude = 20.4, Longitude = 20, TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorValues{Latitude = 16.2, Longitude = 18.9, TripID = dbContext.Trips.Single(x => x.Name == "Moscow Trip").ID},
                    new SensorValues{Latitude = 16.3, Longitude = 18.8, TripID = dbContext.Trips.Single(x => x.Name == "Moscow Trip").ID},
                    new SensorValues{Latitude = 16.4, Longitude = 18.7, TripID = dbContext.Trips.Single(x => x.Name == "Moscow Trip").ID}
                                        
                };

                foreach (SensorValues values in sensorValues)
                {
                    dbContext.SensorValues.Add(values as SensorValues);
                }

                dbContext.SaveChanges();


            }
        }
    }
}
