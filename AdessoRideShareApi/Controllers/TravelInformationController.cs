using AdessoRideShareApi.DbContexts;
using AdessoRideShareApi.Dto;
using AdessoRideShareApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdessoRideShareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelInformationController : ControllerBase
    {
        private readonly RoadTripDbContext roadTripDbContext;

        public TravelInformationController(RoadTripDbContext roadTripDbContext)
        {
            this.roadTripDbContext = roadTripDbContext;
        }

        [HttpGet]
        [Route("GetAllRoadTrips")]
        public async Task<IList<RoadTrip>> GetAllRoadTrips()
        {
            return await roadTripDbContext.RoadTrips.ToListAsync();
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
