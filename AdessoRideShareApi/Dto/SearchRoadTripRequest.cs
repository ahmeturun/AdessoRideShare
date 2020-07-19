using AdessoRideShareApi.Model;

namespace AdessoRideShareApi.Dto
{
    public class SearchRoadTripRequest
    {
        public Location Source { get; set; }
        public Location Destination { get; set; }
    }
}
