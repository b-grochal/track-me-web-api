using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Models;

namespace TrackMeWebAPI.Data
{
    public static class DbSeeder
    {
        public static void SeedData(DbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUserIdentity> userManager)
        {
            SeedApplicationUserRoles(roleManager);
            SeedAdmins(userManager, dbContext);
            SeedBasicUsers(userManager, dbContext);
            SeedTrips(dbContext);
            SeedSensorData(dbContext);
        }

        private static void SeedApplicationUserRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin.ToString())).Wait();
                roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.BasicUser.ToString())).Wait();
            }
        }

        private static void SeedAdmins(UserManager<ApplicationUserIdentity> userManager, DbContext dbContext)
        {   
            if(!dbContext.Admins.Any())
            {
                ApplicationUser admin = new Admin
                {
                    FirstName = "Michael",
                    LastName = "Scott"
                };

                dbContext.ApplicationUsers.Add(admin);
                dbContext.SaveChanges();

                ApplicationUserIdentity adminIdentity = new ApplicationUserIdentity
                {
                    Email = "michael@scott.com",
                    UserName = "michael@scott.com",
                    PhoneNumber = "123-123-123",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    ApplicationUserId = admin.ApplicationUserId
                };
                
                string hashedPassword = userManager.PasswordHasher.HashPassword(adminIdentity, "P@ssw0rd");
                adminIdentity.PasswordHash = hashedPassword;
                userManager.CreateAsync(adminIdentity).Wait();
                userManager.AddToRoleAsync(adminIdentity, ApplicationUserRoles.Admin.ToString()).Wait();
            }
        }

        private static void SeedBasicUsers(UserManager<ApplicationUserIdentity> userManager, DbContext dbContext)
        {
            if (!dbContext.BasicUsers.Any())
            {
                ApplicationUser basicUser = new BasicUser
                {
                    FirstName = "Dwight",
                    LastName = "Schrute",
                };

                dbContext.ApplicationUsers.Add(basicUser);
                dbContext.SaveChanges();

                ApplicationUserIdentity basicUserIdentity = new ApplicationUserIdentity
                {
                    Email = "dwight@schrute.com",
                    UserName = "dwight@schrute.com",
                    PhoneNumber = "123-123-123",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    ApplicationUserId = basicUser.ApplicationUserId
                };

                string hashedPassword = userManager.PasswordHasher.HashPassword(basicUserIdentity, "P@ssw0rd");
                basicUserIdentity.PasswordHash = hashedPassword;
                userManager.CreateAsync(basicUserIdentity).Wait();
                userManager.AddToRoleAsync(basicUserIdentity, ApplicationUserRoles.BasicUser.ToString()).Wait();
            }
        }

        private static void SeedTrips(DbContext dbContext)
        {
            if (!dbContext.Trips.Any())
            {
                List<Trip> trips = new List<Trip>
                {
                    new Trip{ Name="Holiday Trip", BasicUserId = dbContext.Users.Single(x => x.Email.Equals("dwight@schrute.com")).ApplicationUserId },
                    new Trip{ Name="Sunday Trip", BasicUserId = dbContext.Users.Single(x => x.Email.Equals("dwight@schrute.com")).ApplicationUserId },
                    new Trip{ Name="Moscow Trip", BasicUserId = dbContext.Users.Single(x => x.Email.Equals("dwight@schrute.com")).ApplicationUserId }
                };

                foreach(Trip trip in trips)
                {
                    dbContext.Trips.Add(trip as Trip);
                }

                dbContext.SaveChanges();
            }
        }

        private static void SeedSensorData(DbContext dbContext)
        {
            if (!dbContext.SensorData.Any())
            {
                List<SensorData> sensorValues = new List<SensorData>
                {
                    new SensorData{UploadDate = DateTime.Now, Latitude = 50, Longitude = 50, AccelerometerX = 12, AccelerometerY = 12, AccelerometerZ = 12,
                        GyroscopeX = 12, GyroscopeY = 12, GyroscopeZ = 12, MagneticFieldX = 12, MagneticFieldY = 12, MagneticFieldZ = 12,
                        TripId = dbContext.Trips.Single(x => x.Name == "Holiday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 50.2, Longitude = 50, AccelerometerX = 13, AccelerometerY = 13, AccelerometerZ = 13,
                        GyroscopeX = 13, GyroscopeY = 13, GyroscopeZ = 13, MagneticFieldX = 13, MagneticFieldY = 13, MagneticFieldZ = 13,
                        TripId = dbContext.Trips.Single(x => x.Name == "Holiday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 50, Longitude = 50.2, AccelerometerX = 14, AccelerometerY = 14, AccelerometerZ = 14,
                        GyroscopeX = 14, GyroscopeY = 14, GyroscopeZ = 14, MagneticFieldX = 14, MagneticFieldY = 14, MagneticFieldZ = 14,
                        TripId = dbContext.Trips.Single(x => x.Name == "Holiday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 50, Longitude = 50.3, AccelerometerX = 15, AccelerometerY = 15, AccelerometerZ = 15,
                        GyroscopeX = 15, GyroscopeY = 15, GyroscopeZ = 15, MagneticFieldX = 15, MagneticFieldY = 15, MagneticFieldZ = 15,
                        TripId = dbContext.Trips.Single(x => x.Name == "Holiday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 20, Longitude = 20, AccelerometerX = 16, AccelerometerY = 16, AccelerometerZ = 16,
                        GyroscopeX = 16, GyroscopeY = 16, GyroscopeZ = 16, MagneticFieldX = 16, MagneticFieldY = 16, MagneticFieldZ = 16,
                        TripId = dbContext.Trips.Single(x => x.Name == "Sunday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 20.2, Longitude = 20, AccelerometerX = 17, AccelerometerY = 17, AccelerometerZ = 17,
                        GyroscopeX = 17, GyroscopeY = 17, GyroscopeZ = 17, MagneticFieldX = 17, MagneticFieldY = 17, MagneticFieldZ = 17,
                        TripId = dbContext.Trips.Single(x => x.Name == "Sunday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 20.3, Longitude = 20, AccelerometerX = 18, AccelerometerY = 18, AccelerometerZ = 18,
                        GyroscopeX = 18, GyroscopeY = 18, GyroscopeZ = 18, MagneticFieldX = 18, MagneticFieldY = 18, MagneticFieldZ = 18,
                        TripId = dbContext.Trips.Single(x => x.Name == "Sunday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 20.4, Longitude = 20, AccelerometerX = 19, AccelerometerY = 19, AccelerometerZ = 19,
                        GyroscopeX = 19, GyroscopeY = 19, GyroscopeZ = 19, MagneticFieldX = 19, MagneticFieldY = 19, MagneticFieldZ = 19,
                        TripId = dbContext.Trips.Single(x => x.Name == "Sunday Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 16.2, Longitude = 18.9, AccelerometerX = 20, AccelerometerY = 20, AccelerometerZ = 20,
                        GyroscopeX = 20, GyroscopeY = 20, GyroscopeZ = 20, MagneticFieldX = 20, MagneticFieldY = 20, MagneticFieldZ = 20,
                        TripId = dbContext.Trips.Single(x => x.Name == "Moscow Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 16.3, Longitude = 18.8, AccelerometerX = 21, AccelerometerY = 21, AccelerometerZ = 21,
                        GyroscopeX = 21, GyroscopeY = 21, GyroscopeZ = 21, MagneticFieldX = 21, MagneticFieldY = 21, MagneticFieldZ = 21,
                        TripId = dbContext.Trips.Single(x => x.Name == "Moscow Trip").TripId},
                    new SensorData{UploadDate = DateTime.Now, Latitude = 16.4, Longitude = 18.7, AccelerometerX = 22, AccelerometerY = 22, AccelerometerZ = 22,
                        GyroscopeX = 22, GyroscopeY = 22, GyroscopeZ = 22, MagneticFieldX = 22, MagneticFieldY = 22, MagneticFieldZ = 22,
                        TripId = dbContext.Trips.Single(x => x.Name == "Moscow Trip").TripId}                     
                };

                foreach (SensorData values in sensorValues)
                {
                    dbContext.SensorData.Add(values);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
