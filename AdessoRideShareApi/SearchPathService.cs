using AdessoRideShareApi.DbContexts;
using AdessoRideShareApi.Dto;
using AdessoRideShareApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareApi
{
    public class SearchPathService
    {
        private readonly RoadTripDbContext roadTripDbContext;

        public SearchPathService(RoadTripDbContext roadTripDbContext)
        {
            this.roadTripDbContext = roadTripDbContext;
        }

        public async Task<IList<RoadTrip>> SearchPathsInCountry(SearchRoadTripRequest searchRoadTripRequest)
        {
            var roadTrips = await roadTripDbContext.RoadTrips.ToListAsync();
            return roadTrips.Where(roadTrip =>
                ((searchRoadTripRequest.Source.Latitude <= searchRoadTripRequest.Destination.Latitude
                    && roadTrip.Source.Latitude <= searchRoadTripRequest.Source.Latitude
                    && roadTrip.Destination.Latitude >= searchRoadTripRequest.Destination.Latitude)
                || (searchRoadTripRequest.Source.Latitude > searchRoadTripRequest.Destination.Latitude
                    && roadTrip.Source.Latitude >= searchRoadTripRequest.Source.Latitude
                    && roadTrip.Destination.Latitude <= searchRoadTripRequest.Destination.Latitude))
                && ((searchRoadTripRequest.Source.Longtitude <= searchRoadTripRequest.Destination.Longtitude
                    && roadTrip.Source.Longtitude <= searchRoadTripRequest.Source.Longtitude
                    && roadTrip.Destination.Longtitude >= searchRoadTripRequest.Destination.Longtitude)
                || (searchRoadTripRequest.Source.Longtitude > searchRoadTripRequest.Destination.Longtitude
                    && roadTrip.Source.Longtitude >= searchRoadTripRequest.Source.Longtitude
                    && roadTrip.Destination.Longtitude <= searchRoadTripRequest.Destination.Longtitude))
                && roadTrip.PublishStatus).ToList();
        }
    }
}
