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
                    new Trip{Name="Holiday Trip", BasicUserID = dbContext.BasicUsers.Single(x => x.Email.Equals("joe@gmail.com")).ID},
                    new Trip{Name="Sunday Trip", BasicUserID = dbContext.BasicUsers.Single(x => x.Email.Equals("kyle@gmail.com")).ID},
                    new Trip{Name="Moscow Trip", BasicUserID = dbContext.BasicUsers.Single(x => x.Email.Equals("kyle@gmail.com")).ID}
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

            if (!dbContext.SensorsValues.Any())
            {
                List<SensorsValues> sensorValues = new List<SensorsValues>
                {
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 50, Longitude = 50, AccelerometerX = 12, AccelerometerY = 12, AccelerometerZ = 12,
                        GyroscopeX = 12, GyroscopeY = 12, GyroscopeZ = 12, MagneticFieldX = 12, MagneticFieldY = 12, MagneticFieldZ = 12,
                        TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 50.2, Longitude = 50, AccelerometerX = 13, AccelerometerY = 13, AccelerometerZ = 13,
                        GyroscopeX = 13, GyroscopeY = 13, GyroscopeZ = 13, MagneticFieldX = 13, MagneticFieldY = 13, MagneticFieldZ = 13,
                        TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 50, Longitude = 50.2, AccelerometerX = 14, AccelerometerY = 14, AccelerometerZ = 14,
                        GyroscopeX = 14, GyroscopeY = 14, GyroscopeZ = 14, MagneticFieldX = 14, MagneticFieldY = 14, MagneticFieldZ = 14,
                        TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 50, Longitude = 50.3, AccelerometerX = 15, AccelerometerY = 15, AccelerometerZ = 15,
                        GyroscopeX = 15, GyroscopeY = 15, GyroscopeZ = 15, MagneticFieldX = 15, MagneticFieldY = 15, MagneticFieldZ = 15,
                        TripID = dbContext.Trips.Single(x => x.Name == "Holiday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 20, Longitude = 20, AccelerometerX = 16, AccelerometerY = 16, AccelerometerZ = 16,
                        GyroscopeX = 16, GyroscopeY = 16, GyroscopeZ = 16, MagneticFieldX = 16, MagneticFieldY = 16, MagneticFieldZ = 16,
                        TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 20.2, Longitude = 20, AccelerometerX = 17, AccelerometerY = 17, AccelerometerZ = 17,
                        GyroscopeX = 17, GyroscopeY = 17, GyroscopeZ = 17, MagneticFieldX = 17, MagneticFieldY = 17, MagneticFieldZ = 17,
                        TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 20.3, Longitude = 20, AccelerometerX = 18, AccelerometerY = 18, AccelerometerZ = 18,
                        GyroscopeX = 18, GyroscopeY = 18, GyroscopeZ = 18, MagneticFieldX = 18, MagneticFieldY = 18, MagneticFieldZ = 18,
                        TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 20.4, Longitude = 20, AccelerometerX = 19, AccelerometerY = 19, AccelerometerZ = 19,
                        GyroscopeX = 19, GyroscopeY = 19, GyroscopeZ = 19, MagneticFieldX = 19, MagneticFieldY = 19, MagneticFieldZ = 19,
                        TripID = dbContext.Trips.Single(x => x.Name == "Sunday Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 16.2, Longitude = 18.9, AccelerometerX = 20, AccelerometerY = 20, AccelerometerZ = 20,
                        GyroscopeX = 20, GyroscopeY = 20, GyroscopeZ = 20, MagneticFieldX = 20, MagneticFieldY = 20, MagneticFieldZ = 20,
                        TripID = dbContext.Trips.Single(x => x.Name == "Moscow Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 16.3, Longitude = 18.8, AccelerometerX = 21, AccelerometerY = 21, AccelerometerZ = 21,
                        GyroscopeX = 21, GyroscopeY = 21, GyroscopeZ = 21, MagneticFieldX = 21, MagneticFieldY = 21, MagneticFieldZ = 21,
                        TripID = dbContext.Trips.Single(x => x.Name == "Moscow Trip").ID},
                    new SensorsValues{UploadDate = DateTime.Now, Latitude = 16.4, Longitude = 18.7, AccelerometerX = 22, AccelerometerY = 22, AccelerometerZ = 22,
                        GyroscopeX = 22, GyroscopeY = 22, GyroscopeZ = 22, MagneticFieldX = 22, MagneticFieldY = 22, MagneticFieldZ = 22,
                        TripID = dbContext.Trips.Single(x => x.Name == "Moscow Trip").ID}                     
                };

                foreach (SensorsValues values in sensorValues)
                {
                    dbContext.SensorsValues.Add(values as SensorsValues);
                }

                dbContext.SaveChanges();


            }
        }
    }
}
