using System;
namespace AdessoRideShareApi.Model
{
    public class UserRoadTrip
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoadTripId { get; set; }
        public RoadTrip RoadTrip { get; set; }
    }
}
