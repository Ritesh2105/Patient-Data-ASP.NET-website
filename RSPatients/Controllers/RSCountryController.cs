using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RSPatients.Models;

namespace RSPatients.Controllers
{
    // The RSCountryController is used to perform CRUD operations based on Country.cs Model class data
    public class RSCountryController : Controller
    {
        private readonly PatientsContext _context;

        public RSCountryController(PatientsContext context)
        {
            _context = context;
        }

        // GET: RSCountry
        //This is the default 'Index' action.
        public async Task<IActionResult> Index()
        {
            //returns list of Country.cs model class. We are using linq to render it into the view
            return View(await _context.Country.ToListAsync());
        }

        // GET: RSCountry/Details/5
        // Shows the details of the id selected by the user on the screen
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: RSCountry/Create
        // Create GET request shows the form on the screen
        public IActionResult Create()
        {
            return View();
        }

        // POST: RSCountry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // When the user fill data into the form, it comes to Create POST request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            // Checks if data entered is valid, else it will again show the view 
            if (ModelState.IsValid)
            {
                _context.Add(country);
                // save data to the database
                await _context.SaveChangesAsync();
                // redirects to index page
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: RSCountry/Edit/5
        // Used to edit the value based on 'id' selected
        public async Task<IActionResult> Edit(string id)
        {
            // checks id if null
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: RSCountry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Edit POST request is to prefill the data with valid values and save the updated value entered by user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            if (id != country.CountryCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    // save updated value to database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // after updation, redirects to index page
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: RSCountry/Delete/5
        // Used for verify that id selected by user matches with database and is not null
        public async Task<IActionResult> Delete(string id)
        {
            // Checks if id selected is null
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: RSCountry/Delete/5
        // Delete POST request is used to delete the data based on id coming from GET request
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            // Removes the data
            var country = await _context.Country.FindAsync(id);
            _context.Country.Remove(country);
            // Removes data from database
            await _context.SaveChangesAsync();
            //Redirects back to index page
            return RedirectToAction(nameof(Index));
        }
        // Checks if id matches with the country code
        private bool CountryExists(string id)
        {
            return _context.Country.Any(e => e.CountryCode == id);
        }
    }
}
