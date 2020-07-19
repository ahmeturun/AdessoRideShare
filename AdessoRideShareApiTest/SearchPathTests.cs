using AdessoRideShareApi;
using AdessoRideShareApi.DbContexts;
using AdessoRideShareApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdessoRideShareApiTest
{
    public class Tests
    {
        private ServiceProvider serviceProvider { get; set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTransient<SearchPathService>();
            services.AddDbContext<RoadTripDbContext>(options =>
            {
                options.UseSqlite("Filename=TestDatabase.db", options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
                options.EnableSensitiveDataLogging();
            });
            serviceProvider = services.AddLogging().BuildServiceProvider();

            string dbName = "TestDatabase.db";
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            var roadTripContextOpts = new DbContextOptionsBuilder<RoadTripDbContext>().UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            }).Options;

            AddInitialRoadTrips(serviceProvider.GetRequiredService<RoadTripDbContext>());
        }

        [Test]
        public async Task SearchPathsInCountryTest()
        {
            var dbContext = serviceProvider.GetRequiredService<RoadTripDbContext>();
            var allRoadTrips = await dbContext.RoadTrips.ToListAsync();
            var searchPathService = serviceProvider.GetRequiredService<SearchPathService>();
            var result = await searchPathService.SearchPathsInCountry(new AdessoRideShareApi.Dto.SearchRoadTripRequest
            {
                Source = new Location { Latitude = 50, Longtitude = 50 },
                Destination = new Location { Latitude = 250, Longtitude = 250 }
            });

            Assert.IsTrue(result.Count == 2);
        }

        private void AddInitialRoadTrips(RoadTripDbContext roadTripDbContext)
        {
            roadTripDbContext.Database.EnsureCreated();
            roadTripDbContext.Users.Add(new User
            {
                Name = "test user",
                ContactDetails = "foo street, bar district"
            });
            roadTripDbContext.SaveChanges();
            var someUser = roadTripDbContext.Users.FirstAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            for (int index = 0; index < 500; index += 50)
            {
                roadTripDbContext.RoadTrips.Add(new RoadTrip
                {
                    User = someUser,
                    Source = new Location { Latitude = index, Longtitude = index },
                    Destination = new Location { Latitude = 500 - index, Longtitude = 500 - index },
                    TripDateTime = DateTime.UtcNow,
                    TravelerCapacity = 3,
                    Details = "going fro point foo to point bar.",
                    PublishStatus = true
                });
            }
            roadTripDbContext.SaveChanges();
        }
    }
}