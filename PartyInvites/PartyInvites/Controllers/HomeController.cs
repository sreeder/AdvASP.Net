using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            //object initializer use this to make sure that all objects are fully initialized

            var vm = new IndexModel
            {
                CurrentTime = DateTime.Now,
                Greeting = DateTime.Now.Hour < 12 ? "Good Morning" : "Good Afternoon"
            };
        

            return View(vm);
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                //TODO: EMail response to the party organizer
                return View("Thanks", guestResponse);
            }else
            {
                //there is a validation error
                return View();
            }
        }
    }
}