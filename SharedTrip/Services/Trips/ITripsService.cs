using SharedTrip.ViewModel.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services.Trips
{
    public interface ITripsService
    {
        void Add(string startPoint, string endPoint, DateTime departureTime,
            int seats, string description, string imagePath);

        IEnumerable<TripInfoViewModel> All();

        TripDetailsViewModel Details(string tripId);
    }
}
