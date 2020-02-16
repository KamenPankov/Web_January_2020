using SharedTrip.Services.Trips;
using SharedTrip.Services.Users;
using SharedTrip.Services.UsersTrips;
using SharedTrip.ViewModel.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;
        private readonly IUsersTripsService usersTripsService;
        private readonly IUsersService usersService;

        public TripsController(ITripsService tripsService, IUsersTripsService usersTripsService, IUsersService usersService)
        {
            this.tripsService = tripsService;
            this.usersTripsService = usersTripsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public HttpResponse Add()
        {

            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(inputModel.StartPoint) ||
                string.IsNullOrEmpty(inputModel.EndPoint) ||
                string.IsNullOrEmpty(inputModel.DepartureTime) ||
                string.IsNullOrEmpty(inputModel.DepartureTime))
            {
                return this.Redirect("/Trips/Add");
            }

            //if(DateTime.TryParse(inputModel.DepartureTime, out DateTime departureTime))
            if (!DateTime.TryParseExact(inputModel.DepartureTime, "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureTime))
            {
                return this.Redirect("/Trips/Add");
            }

            if (inputModel.Seats < 2 || inputModel.Seats > 6)
            {
                return this.Redirect("/Trips/Add");
            }

            if (inputModel.Description.Length > 80)
            {
                return this.Redirect("/Trips/Add");
            }

            this.tripsService.Add(inputModel.StartPoint, inputModel.EndPoint, departureTime,
                inputModel.Seats, inputModel.Description, inputModel.ImagePath);

            return this.Redirect("/Trips/All");
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            TripsAllViewModel tripsAllViewModel = new TripsAllViewModel()
            {
                Trips = this.tripsService.All()
            };

            return this.View(tripsAllViewModel);
        }

        [HttpGet]
        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            TripDetailsViewModel tripDetailsViewModel = this.tripsService.Details(tripId);

            return this.View(tripDetailsViewModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (this.usersService.HasJoinedTrip(this.User, tripId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.usersTripsService.AddUserToTrip(this.User, tripId);

            return this.Redirect("/Trips/All");
        }
    }
}
