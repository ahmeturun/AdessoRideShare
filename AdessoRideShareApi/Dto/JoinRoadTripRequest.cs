using AdessoRideShareApi.Model;

namespace AdessoRideShareApi.Dto
{
    public class JoinRoadTripRequest
    {
        public long RoadTripId { get; set; }
        public User JoiningUser { get; set; }
    }
}
