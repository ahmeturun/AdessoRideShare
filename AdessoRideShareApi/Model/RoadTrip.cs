﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdessoRideShareApi.Model
{
    /// <summary>
    /// Repsesents road trip infomation model
    /// </summary>
    public class RoadTrip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Location Source { get; set; }
        [Required]
        public Location Destination { get; set; }
        [Required]
        public DateTime TripDateTime { get; set; }
        [Required]
        public int TravelerCapacity { get; set; }
        public ICollection<UserRoadTrip> JoinedTravelers { get; set; }
        public string Details { get; set; }
        public bool PublishStatus { get; set; }

    }
}
