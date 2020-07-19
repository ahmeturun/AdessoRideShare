using AdessoRideShareApi.DbContexts;
using AdessoRideShareApi.Dto;
using AdessoRideShareApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<RoadTrip> AddRoadTrip([FromBody]RoadTrip roadTrip)
        {
            var entityContext = roadTripDbContext.Add(roadTrip);
            await roadTripDbContext.SaveChangesAsync();
            return entityContext.Entity;
        }

        [HttpPut]
        [Route("UpdateRoadTrip")]
        public async Task<RoadTrip> UpdateRoadTrip([FromBody]RoadTrip roadTrip)
        {
            var entityContext = roadTripDbContext.Update(roadTrip);
            await roadTripDbContext.SaveChangesAsync();
            return entityContext.Entity;
        }

        [HttpPost]
        [Route("JoinRoadTrip")]
        public async Task<JoinRoadTripResponse> JoinRoadTrip([FromBody] JoinRoadTripRequest joinRoadTripRequest)
        {
            var entityContext = await roadTripDbContext.FindAsync<RoadTrip>(joinRoadTripRequest.RoadTripId);
            if(entityContext.JoinedTravelers?.Count >= entityContext.TravelerCapacity
                || !entityContext.PublishStatus)
            {
                return new JoinRoadTripResponse
                {
                    Result = false,
                    Details = "this journey is not available."
                };
            }
            entityContext.JoinedTravelers.Add(new UserRoadTrip()
            {
                RoadTripId = joinRoadTripRequest.RoadTripId,
                UserId = joinRoadTripRequest.JoiningUserID
            });
            await roadTripDbContext.SaveChangesAsync();
            return new JoinRoadTripResponse
            {
                Result = true
            };
        }

        [HttpPost]
        [Route("SearchRoadTrip")]
        public async Task<IList<RoadTrip>> SearchRoadTrip([FromBody] SearchRoadTripRequest searchRoadTripRequest)
        {
            return await roadTripDbContext.RoadTrips.Where(roadTrip => 
                (roadTrip.Source == searchRoadTripRequest.Source 
                || roadTrip.Destination == searchRoadTripRequest.Destination)
                && roadTrip.PublishStatus)
                .ToListAsync();
        }
    }
}
