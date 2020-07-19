using AdessoRideShareApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdessoRideShareApi.DbContexts
{
    public class RoadTripDbContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory;

        public RoadTripDbContext(DbContextOptions<RoadTripDbContext> options,
            ILoggerFactory loggerFactory) : base (options)
        {
            this.loggerFactory = loggerFactory;
        }

        public DbSet<RoadTrip> RoadTrips { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoadTrip>().ToTable("RoadTrip", "test");
            modelBuilder.Entity<RoadTrip>(entity =>
            {
                entity.HasKey(item => item.Id);
                entity.HasOne(item => item.User).WithMany();
                entity.Property(item => item.Source).HasJsonConversion();
                entity.Property(item => item.Destination).HasJsonConversion();
                entity.Property(item => item.TripDateTime);
                entity.Property(item => item.TravelerCapacity);
                entity.Property(item => item.Details);
                entity.Property(item => item.PublishStatus);
            });

            modelBuilder.Entity<User>().ToTable("User", "test");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(item => item.Id);
                entity.Property(item => item.Name);
                entity.Property(item => item.ContactDetails);
            });

            modelBuilder.Entity<UserRoadTrip>()
                .HasKey(userRoadTrip => new { userRoadTrip.UserId, userRoadTrip.RoadTripId});
            modelBuilder.Entity<UserRoadTrip>()
                .HasOne(userRoadTrip => userRoadTrip.User)
                .WithMany(user => user.JoinedRoadTrips)
                .HasForeignKey(userRoadTrip => userRoadTrip.RoadTripId);
            modelBuilder.Entity<UserRoadTrip>()
                .HasOne(userRoadTrip => userRoadTrip.RoadTrip)
                .WithMany(roadTrip => roadTrip.JoinedTravelers)
                .HasForeignKey(userRoadTrip => userRoadTrip.UserId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
