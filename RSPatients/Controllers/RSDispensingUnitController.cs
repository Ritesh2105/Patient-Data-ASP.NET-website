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
    // The RSDispensingUnitController is used to perform CRUD operations based on DispensingUnit.cs Model class data
    public class RSDispensingUnitController : Controller
    {
        private readonly PatientsContext _context;

        public RSDispensingUnitController(PatientsContext context)
        {
            _context = context;
        }

        // GET: RSDispensingUnit
        //This is the default 'Index' action.
        public async Task<IActionResult> Index()// used for displaying the list of the data
        {
            //returns list of DispensingUnit.cs model class. We are using linq to render it into the view
            return View(await _context.DispensingUnit.ToListAsync());
        }

        // GET: RSDispensingUnit/Details/5
        // Shows the details of the id selected by the user on the screen
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnit
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }

            return View(dispensingUnit);
        }

        // GET: RSDispensingUnit/Create
        // Create GET request shows the form on the screen
        public IActionResult Create()
        {
            return View();
        }

        // POST: RSDispensingUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Edit POST request is to prefill the data with valid values and save the updated value entered by user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DispensingCode")] DispensingUnit dispensingUnit)
        {
            // Checks if data entered is valid, else it will again show the view 
            if (ModelState.IsValid)
            {
                _context.Add(dispensingUnit);
                // save data to the database
                await _context.SaveChangesAsync();
                // redirects to index page
                return RedirectToAction(nameof(Index));
            }
            return View(dispensingUnit);
        }

        // GET: RSDispensingUnit/Edit/5
        // Used to edit the value based on 'id' selected
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnit.FindAsync(id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }
            return View(dispensingUnit);
        }

        // POST: RSDispensingUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DispensingCode")] DispensingUnit dispensingUnit)
        {
            if (id != dispensingUnit.DispensingCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispensingUnit);
                    // save updated value to database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispensingUnitExists(dispensingUnit.DispensingCode))
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
            return View(dispensingUnit);
        }

        // GET: RSDispensingUnit/Delete/5
        // Used for verify that id selected by user matches with database and is not null
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnit
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }

            return View(dispensingUnit);
        }

        // POST: RSDispensingUnit/Delete/5
        // Delete POST request is used to delete the data based on id coming from GET request
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dispensingUnit = await _context.DispensingUnit.FindAsync(id);
            _context.DispensingUnit.Remove(dispensingUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Checks if id matches with the Dispensing code
        private bool DispensingUnitExists(string id)
        {
            return _context.DispensingUnit.Any(e => e.DispensingCode == id);
        }
    }
}
