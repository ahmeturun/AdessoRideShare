using AdessoRideShareApi.DbContexts;
using AdessoRideShareApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdessoRideShareApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitializeDbWithDefaults();
            CreateHostBuilder(args).Build().Run();
        }

        private static void InitializeDbWithDefaults()
        {
            string dbName = "TestDatabase.db";
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            var roadTripContextOpts = new DbContextOptionsBuilder<RoadTripDbContext>().UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            }).Options;
            using (var roadTripDbContext = new RoadTripDbContext(roadTripContextOpts, new LoggerFactory()))
            {
                roadTripDbContext.Database.EnsureCreated();
                if (roadTripDbContext.Users.CountAsync().ConfigureAwait(false).GetAwaiter().GetResult() <= 0)
                {
                    roadTripDbContext.Users.Add(new Model.User
                    {
                        Name = "test user",
                        ContactDetails = "foo street, bar district"
                    });
                    roadTripDbContext.Users.Add(new Model.User
                    {
                        Name = "test joined user",
                        ContactDetails = "bar street, foo district"
                    });
                }
                roadTripDbContext.SaveChanges();
                if (roadTripDbContext.RoadTrips.CountAsync().ConfigureAwait(false).GetAwaiter().GetResult() <= 0)
                {
                    var someUser = roadTripDbContext.Users.FirstAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    roadTripDbContext.RoadTrips.Add(new Model.RoadTrip
                    {
                        User = someUser,
                        Source = new Model.Location { Latitude = 50, Longtitude = 50 },
                        Destination = new Model.Location { Latitude = 200, Longtitude = 150 },
                        TripDateTime = DateTime.UtcNow,
                        TravelerCapacity = 3,
                        Details = "going fro point foo to point bar.",
                        PublishStatus = false
                    });
                    roadTripDbContext.RoadTrips.Add(new Model.RoadTrip
                    {
                        User = someUser,
                        Source = new Model.Location { Latitude = 0, Longtitude = 0 },
                        Destination = new Model.Location { Latitude = 200, Longtitude = 200 },
                        TripDateTime = DateTime.UtcNow,
                        TravelerCapacity = 3,
                        Details = "going fro point foo to point bar.",
                        PublishStatus = false
                    });
                }
                roadTripDbContext.SaveChanges();

                var someJoinedUser = roadTripDbContext.Users.ToListAsync().ConfigureAwait(false).GetAwaiter().GetResult().LastOrDefault();
                var roadTrip = roadTripDbContext.RoadTrips.FirstAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                roadTrip.JoinedTravelers = new List<UserRoadTrip>{
                    new UserRoadTrip()
                    {
                        User = someJoinedUser,
                        RoadTrip = roadTrip
                    }
                };
                roadTripDbContext.SaveChanges();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
