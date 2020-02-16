using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services.Users
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(string username, string password, string email)
        {
            User user = new User()
            {
                Username = username,
                Password = this.Hash(password),
                Email = email
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool ContainsEmail(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public bool ContainsUsername(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        public string GetUserId(string username, string password)
        {
            password = this.Hash(password);

            return this.db.Users
                .Where(u => u.Username == username &&
                                            u.Password == password)
                .Select(u => u.Id)
                .FirstOrDefault();
        }

        public string GetUsername(string id)
        {
            

            return this.db.Users
                .Where(u => u.Id == id)
                .Select(u => u.Username)
                .FirstOrDefault();
        }

        public bool HasJoinedTrip(string userId, string tripId)
        {
            return this.db.UserTrips.Any(ut => ut.UserId == userId && ut.TripId == tripId);
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }

    }
}
