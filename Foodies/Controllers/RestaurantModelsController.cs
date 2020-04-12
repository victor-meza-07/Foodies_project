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
            return View(await _context.Restaurants.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("RestaurantModelPrimaryKey,RestaurantName,RestaurantPhone,AddressKey,PriceRangeIndex,WebsiteUrl,MenuUrl,GoogleGeoLocationData")] RestaurantModel restaurantModel)
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
        public async Task<IActionResult> Edit(string id, [Bind("RestaurantModelPrimaryKey,RestaurantName,RestaurantPhone,AddressKey,PriceRangeIndex,WebsiteUrl,MenuUrl,GoogleGeoLocationData")] RestaurantModel restaurantModel)
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
