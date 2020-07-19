using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdessoRideShareApi.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactDetails { get; set; }
        public ICollection<UserRoadTrip> JoinedRoadTrips { get; set; }
    }
}