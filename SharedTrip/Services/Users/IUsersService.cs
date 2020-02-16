using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services.Users
{
    public interface IUsersService
    {
        void Add(string username, string password, string email);

        string GetUsername(string id);

        

        string GetUserId(string username, string password);

        

        bool ContainsUsername(string username);

        bool ContainsEmail(string email);

        bool HasJoinedTrip(string userId, string tripId);
    }
}
