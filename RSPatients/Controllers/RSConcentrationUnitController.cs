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
    // The RSConcentrationUnitController is used to perform CRUD operations based on ConcentrtionUnit.cs Model class data
    public class RSConcentrationUnitController : Controller
    {
        private readonly PatientsContext _context;

        public RSConcentrationUnitController(PatientsContext context)
        {
            _context = context;
        }

        // GET: RSConcentrationUnit0
        //This is the default 'Index' action.
        public async Task<IActionResult> Index()
        {
            //returns list of ConcentrtionUnit.cs model class. We are using linq to render it into the view
            return View(await _context.ConcentrationUnit.ToListAsync());
        }

        // GET: RSConcentrationUnit/Details/5
        // Shows the details of the id selected by the user on the screen
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnit
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // GET: RSConcentrationUnit/Create
        // Create GET request shows the form on the screen
        public IActionResult Create()
        {
            return View();
        }

        // POST: RSConcentrationUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Edit POST request is to prefill the data with valid values and save the updated value entered by user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            // Checks if data entered is valid, else it will again show the view 
            if (ModelState.IsValid)
            {
                _context.Add(concentrationUnit);
                // save data to the database
                await _context.SaveChangesAsync();
                // redirects to index page
                return RedirectToAction(nameof(Index));
            }
            return View(concentrationUnit);
        }

        // GET: RSConcentrationUnit/Edit/5
        // Used to edit the value based on 'id' selected
        public async Task<IActionResult> Edit(string id)
        {
            // checks id if null
            if (id == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnit.FindAsync(id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }
            return View(concentrationUnit);
        }

        // POST: RSConcentrationUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            if (id != concentrationUnit.ConcentrationCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concentrationUnit);
                    // save updated value to database
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcentrationUnitExists(concentrationUnit.ConcentrationCode))
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
            return View(concentrationUnit);
        }

        // GET: RSConcentrationUnit/Delete/5
        // Used for verify that id selected by user matches with database and is not null
        public async Task<IActionResult> Delete(string id)
        {
            // Checks if id selected is null
            if (id == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnit
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // POST: RSConcentrationUnit/Delete/5
        // Delete POST request is used to delete the data based on id coming from GET request
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            // Removes the data
            var concentrationUnit = await _context.ConcentrationUnit.FindAsync(id);
            _context.ConcentrationUnit.Remove(concentrationUnit);
            // Removes data from database
            await _context.SaveChangesAsync();
            //Redirects back to index page
            return RedirectToAction(nameof(Index));
        }
        // Checks if id matches with the concentration code
        private bool ConcentrationUnitExists(string id)
        {
            return _context.ConcentrationUnit.Any(e => e.ConcentrationCode == id);
        }
    }
}
