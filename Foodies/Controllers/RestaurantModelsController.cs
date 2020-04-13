using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Foodies.Data;
using Foodies.Models;

namespace Foodies.Controllers
{
    public class RestaurantModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantModels
        public async Task<IActionResult> Index()
        {
            PredictiveModel model = new PredictiveModel(_context);


            //Find out what customer is using the app at the current moment
            /* var customer = _context.Customers.Where(/***here is where you figure that part out, ask an instructor**//*);*/

            string sampleGUID = "35asd684as3da8sd43ads68a4sd3";
            var customer = _context.Customers.Where(c => c.CustomerModelPrimaryKey == sampleGUID).FirstOrDefault();
            var listOfRecomendations = model.GetRestaurantRecomendations(customer);
            return View(listOfRecomendations);
        }

        // GET: RestaurantModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantModel = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantModelPrimaryKey == id);
            if (restaurantModel == null)
            {
                return NotFound();
            }

            return View(restaurantModel);
        }

        // GET: RestaurantModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RestaurantModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantModelPrimaryKey,RestaurantName,RestaurantPhone,AddressKey,PriceRangeIndex,WebsiteUrl,Open_now,Lat,Lng,Price_level,Rating,Place_Id")] RestaurantModel restaurantModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurantModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurantModel);
        }

        // GET: RestaurantModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantModel = await _context.Restaurants.FindAsync(id);
            if (restaurantModel == null)
            {
                return NotFound();
            }
            return View(restaurantModel);
        }

        // POST: RestaurantModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RestaurantModelPrimaryKey,RestaurantName,RestaurantPhone,AddressKey,PriceRangeIndex,WebsiteUrl,Open_now,Lat,Lng,Price_level,Rating,Place_Id")] RestaurantModel restaurantModel)
        {
            if (id != restaurantModel.RestaurantModelPrimaryKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantModelExists(restaurantModel.RestaurantModelPrimaryKey))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurantModel);
        }

        // GET: RestaurantModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantModel = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantModelPrimaryKey == id);
            if (restaurantModel == null)
            {
                return NotFound();
            }

            return View(restaurantModel);
        }

        // POST: RestaurantModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var restaurantModel = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurantModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantModelExists(string id)
        {
            return _context.Restaurants.Any(e => e.RestaurantModelPrimaryKey == id);
        }
    }
}
