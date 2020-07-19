using System;

namespace AdessoRideShareApi.Dto
{
    public class JoinRoadTripRequest
    {
        public Guid RoadTripId { get; set; }
        public Guid JoiningUserID { get; set; }
    }
}
