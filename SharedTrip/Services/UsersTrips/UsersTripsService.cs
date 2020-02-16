using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services.UsersTrips
{
    public class UsersTripsService : IUsersTripsService
    {
        private ApplicationDbContext db;

        public UsersTripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddUserToTrip(string userId, string tripId)
        {
            this.db.UserTrips.Add(new UserTrip()
            {
                UserId = userId,
                TripId = tripId
            });

            this.db.SaveChanges();
        }
    }
}
