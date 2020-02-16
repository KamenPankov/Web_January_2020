namespace SharedTrip
{
    using System.Collections.Generic;
    using SharedTrip.Services.Trips;
    using SharedTrip.Services.Users;
    using SharedTrip.Services.UsersTrips;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ITripsService, TripsService>();
            serviceCollection.Add<IUsersTripsService, UsersTripsService>();
        }
    }
}
