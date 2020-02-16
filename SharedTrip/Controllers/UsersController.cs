using SharedTrip.Services.Users;
using SharedTrip.ViewModel.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public HttpResponse Login()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserLoggedIn())
            {
                return Redirect("/");
            }

            string userId = this.usersService.GetUserId(username, password);

            if (userId != null)
            {
                this.SignIn(userId);

                //TODO: redirect to /Trips/All
                return this.Redirect("/");
            }
            else
            {
                return this.Redirect("/Users/Login");
            }
        }

        [HttpGet]
        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserInputModel inputModel)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (inputModel.Username?.Length < 5 || inputModel.Username?.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (inputModel.Password?.Length < 6 || inputModel.Password?.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.IsEmailValid(inputModel.Email))
            {
                return this.Redirect("/Users/Register");
            }

            if (this.usersService.ContainsEmail(inputModel.Email))
            {
                return this.Redirect("/Users/Register");
            }

            if (this.usersService.ContainsUsername(inputModel.Username))
            {
                return this.Redirect("/Users/Register");
            }

            this.usersService.Add(inputModel.Username,
                inputModel.Password, inputModel.Email);

           
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/");
            }

            this.SignOut();

            return Redirect("/");
        }


        private bool IsEmailValid(string emailaddress)
        {
            try
            {
                new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
