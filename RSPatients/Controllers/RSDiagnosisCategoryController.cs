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
    // The RSDiagnosisCategoryController is used to perform CRUD operations based on Diagnosis.cs Model
    public class RSDiagnosisCategoryController : Controller
    {
        private readonly PatientsContext _context;

        public RSDiagnosisCategoryController(PatientsContext context)
        {
            _context = context;
        }

        // GET: RSDiagnosisCategory
        //This is the default 'Index' action.
        public async Task<IActionResult> Index()
        {
            //returns list of Diagnosis.cs model class. We are using linq to render it into the view
            return View(await _context.DiagnosisCategory.ToListAsync());
        }

        // GET: RSDiagnosisCategory/Details/5
        // Shows the details of the id selected by the user on the screen
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // GET: RSDiagnosisCategory/Create
        // Create GET request shows the form on the screen
        public IActionResult Create()
        {
            return View();
        }

        // POST: RSDiagnosisCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Edit POST request is to prefill the data with valid values and save the updated value entered by user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            // Checks if data entered is valid, else it will again show the view 
            if (ModelState.IsValid)
            {
                _context.Add(diagnosisCategory);
                // save data to the database
                await _context.SaveChangesAsync();
                // redirects to index page
                return RedirectToAction(nameof(Index));
            }
            return View(diagnosisCategory);
        }

        // GET: RSDiagnosisCategory/Edit/5
        // Used to edit the value based on 'id' selected
        public async Task<IActionResult> Edit(int? id)
        {
            // checks id if null
            if (id == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategory.FindAsync(id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }
            return View(diagnosisCategory);
        }

        // POST: RSDiagnosisCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            if (id != diagnosisCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // save updated value to database
                    _context.Update(diagnosisCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosisCategoryExists(diagnosisCategory.Id))
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
            return View(diagnosisCategory);
        }

        // GET: RSDiagnosisCategory/Delete/5
        // Used for verify that id selected by user matches with database and is not null
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // POST: RSDiagnosisCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnosisCategory = await _context.DiagnosisCategory.FindAsync(id);
            _context.DiagnosisCategory.Remove(diagnosisCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Checks if id matches with the Diagnosis id
        private bool DiagnosisCategoryExists(int id)
        {
            return _context.DiagnosisCategory.Any(e => e.Id == id);
        }
    }
}
