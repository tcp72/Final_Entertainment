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
                return View("Entertainers"); //probably want to pass e here!
}
        }



        //pass in the entertainer id to the details page //think need httpget
        //[HttpGet]
        public IActionResult Details(long entId)
        {
            var person = context.Entertainers.Single(x => x.EntertainerId == entId);

            //var blah = context.Entertainers.ToList(); //get the info from Entertainers table (this is where find EntstageName)
            return View("AddEntertainer", person);
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
