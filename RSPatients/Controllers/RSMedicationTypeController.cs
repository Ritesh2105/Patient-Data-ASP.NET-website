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
    // The RSMedicationTypeController is used to perform CRUD operations based on MedicationType.cs Model class data
    public class RSMedicationTypeController : Controller
    {
        private readonly PatientsContext _context;

        public RSMedicationTypeController(PatientsContext context)
        {
            _context = context;
        }

        // GET: RSMedicationType
        //This is the default 'Index' action.
        public async Task<IActionResult> Index()
        {
            //returns list of MedicationType.cs model class. We are using linq to render it into the view
            return View(await _context.MedicationType.ToListAsync());
        }

        // GET: RSMedicationType/Details/5
        // Shows the details of the id selected by the user on the screen
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationType
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // GET: RSMedicationType/Create
        // Create GET request shows the form on the screen
        public IActionResult Create()
        {
            return View();
        }

        // POST: RSMedicationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            // Checks if data entered is valid, else it will again show the view 
            if (ModelState.IsValid)
            {
                _context.Add(medicationType);
                // save data to the database
                await _context.SaveChangesAsync();
                // redirects to index page
                return RedirectToAction(nameof(Index));
            }
            return View(medicationType);
        }

        // GET: RSMedicationType/Edit/5
        // Used to edit the value based on 'id' selected
        public async Task<IActionResult> Edit(int? id)
        {
            // checks id if null
            if (id == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationType.FindAsync(id);
            if (medicationType == null)
            {
                return NotFound();
            }
            return View(medicationType);
        }

        // POST: RSMedicationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Edit POST request is to prefill the data with valid values and save the updated value entered by user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (id != medicationType.MedicationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicationType);
                    // save updated value to database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationTypeExists(medicationType.MedicationTypeId))
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
            return View(medicationType);
        }

        // GET: RSMedicationType/Delete/5
        // Used for verify that id selected by user matches with database and is not null
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationType
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // POST: RSMedicationType/Delete/5
        // Delete POST request is used to delete the data based on id coming from GET request
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicationType = await _context.MedicationType.FindAsync(id);
            _context.MedicationType.Remove(medicationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Checks if id matches with the Medication Type id
        private bool MedicationTypeExists(int id)
        {
            return _context.MedicationType.Any(e => e.MedicationTypeId == id);
        }
    }
}
