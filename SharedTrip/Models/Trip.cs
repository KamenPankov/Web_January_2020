using System;
using System.Collections.Generic;

namespace SharedTrip.Models
{

    public class Trip
    {

        public Trip()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserTrips = new HashSet<UserTrip>();
        }

        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<UserTrip> UserTrips { get; set; }
    }
}
