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
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;

namespace Foodies.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        //post
        public async Task<IActionResult> Index() 
        {
            

            return View();
        }


        public IActionResult RedirectoToFacebook()
        {
            Models.Services.FacebookDataRequest facebookDataRequest = new Models.Services.FacebookDataRequest(_context);
            facebookDataRequest.postman();

            return View("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Register() 
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
