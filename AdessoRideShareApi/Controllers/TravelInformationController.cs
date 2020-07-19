using AdessoRideShareApi.Dto;
using AdessoRideShareApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AdessoRideShareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelInformationController : ControllerBase
    {
        [HttpGet]
        [Route("GetAllRoadTrips")]
        public IList<RoadTrip> GetAllRoadTrips()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("AddRoadTrip")]
        public RoadTrip AddRoadTrip([FromBody]RoadTrip roadTrip)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("UpdateRoadTrip")]
        public RoadTrip UpdateRoadTrip([FromBody]RoadTrip roadTrip)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("JoinRoadTrip")]
        public void JoinRoadTrip([FromBody] JoinRoadTripRequest joinRoadTripRequest)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("SearchRoadTrip")]
        public IList<RoadTrip> SearchRoadTrip([FromBody] SearchRoadTripRequest searchRoadTripRequest)
        {
            throw new NotImplementedException();
        }
    }
}
