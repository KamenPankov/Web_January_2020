using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services.UsersTrips
{
    public interface IUsersTripsService
    {
        void AddUserToTrip(string userId, string tripId);
    }
}
