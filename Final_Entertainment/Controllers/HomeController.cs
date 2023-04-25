using Final_Entertainment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Entertainment.Controllers
{
    public class HomeController : Controller
    {
        //I added
        public Entertainers_Context context { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, Entertainers_Context temp)
        {
            _logger = logger;
            context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Entertainers action
        public IActionResult Entertainers()
        {
            var blah = context.Entertainers.ToList(); //get the info from Entertainers table (this is where find EntstageName)
            return View(blah);
        }

        //Add Entertainer Get
        [HttpGet]
        public IActionResult AddEntertainer()
        {
            return View();
        }

        //Add Entertainer Post
        [HttpPost]
        public IActionResult AddEntertainer(Entertainers e) //create instance of Entertainers
        {
            if (ModelState.IsValid) //model validation
            {
                context.Add(e);
                context.SaveChanges();

                return View("Confirmation");
            }
            else
            {
                return RedirectToAction("Entertainers"); //probably want to pass e here!
            }
        }


        //pass in the entertainer id to the details page
        [HttpGet]
        public IActionResult Details(long entId)
        {
            var person = context.Entertainers.Single(x => x.EntertainerId == entId);

            //var blah = context.Entertainers.ToList(); //get the info from Entertainers table (this is where find EntstageName)
            return View("Details", person);
        }

        //this is the edit button and we're saving it
        [HttpPost]
        public IActionResult Details (Entertainers blah)
        {
            if (ModelState.IsValid)
            {
                context.Update(blah); //update changes based on info passed in
                context.SaveChanges(); //save those changes

                return RedirectToAction("Entertainers"); //send user to WaitList Action and load the data needed to make page display
            }
            else
            {
                return RedirectToAction("Entertainers");
            }
        }

        //Delete functionality
        public IActionResult Delete (Entertainers e)
        {
            context.Entertainers.Remove(e);
            context.SaveChanges();

            return RedirectToAction("Delete_Successful");
        }

        //Delete Successful
        public IActionResult Delete_Successful()
        {
            return RedirectToAction("Entertainers");
        }

        //-------------------don't care about below---------I kept it because my program broken when I removed it (please don't dock for dirty code here :)
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
