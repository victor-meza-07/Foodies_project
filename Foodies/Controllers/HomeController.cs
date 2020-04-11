using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Foodies.Models;
using Foodies.Data;
using Foodies.Contracts;

namespace Foodies.Controllers
{
    public class HomeController : Controller
    {
        
        private IPlacesRequest _placesRequest;
        public HomeController(IPlacesRequest placesRequest)
        {
            _placesRequest = placesRequest;
        }


        //post
        public async Task<IActionResult> Index()
        {

            //Call the facebook method.


            //How to display the information you retrieve from google. 
          

            return View();
        }


        



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
