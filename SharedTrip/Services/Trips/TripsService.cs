using SharedTrip.Models;
using SharedTrip.ViewModel.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTrip.Services.Trips
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(string startPoint, string endPoint, DateTime departureTime, int seats, string description, string imagePath)
        {
            this.db.Trips.Add(new Trip()
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                DepartureTime = departureTime,
                Seats = seats,
                Description = description,
                ImagePath = imagePath
            });

            this.db.SaveChanges();
        }

        public IEnumerable<TripInfoViewModel> All()
        {
            return this.db.Trips
                .Select(t => new TripInfoViewModel()
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats
                })
                .ToArray();
        }

        public TripDetailsViewModel Details(string tripId)
        {
            return this.db.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsViewModel()
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats,
                    Description = t.Description,
                    ImagePath = t.ImagePath
                })
                .FirstOrDefault();
        }
    }
}
