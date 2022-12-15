// This controller is largely based on a scaffolded class created using Entity Framework.
// Edits have been made, but I had Visual Studio generate the bones.

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrismaPicker.Data;
using PrismaPicker.Models;

namespace PrismaPicker.Controllers
{
    // Every method in this class except those with [AllowAnonymous] annotation require a user to be signed in to access
    // Attempting to access a view which is protected by an [Authorize] method will redirect to /Account/Login view instead
    [Authorize]
    public class GlassController : Controller
    {
        private readonly PrismaPickerContext _context;

        // Constructor with DbContext
        public GlassController(PrismaPickerContext context)
        {
            _context = context;
        }

        // GET method displays /Glass/Index and handles filtering based on a search string parameter 
        [AllowAnonymous]
        public async Task<IActionResult> Index(string search)   
        {
            ViewBag.Title = "PrismaPicker | Catalog";

            var glass = from g in _context.Glass select g;

            // Filter results if search parameter is not null/empty
            if (!String.IsNullOrEmpty(search))
            {
                glass = glass.Where(s => s.Name!.Contains(search));
            }

            return View(await glass.ToListAsync());
        }

        // GET method displays /Glass/Details/[ID]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            // Null check to send a 404 response if there's no ID
            if (id == null)
            {
                return NotFound();
            }

            // Pull glass record using the passed ID
            var glass = await _context.Glass.FirstOrDefaultAsync(m => m.Id == id);

            // Null check sends 404 response if ID isn't associated with a glass or something has otherwise gone belly up
            if (glass == null)
            {
                return NotFound();
            }

            ViewBag.Title = "PrismaPicker | " + glass.Name;
            return View(glass);
        }

        // GET method for displays /Glass/Create
        public IActionResult Create()
        {
            ViewBag.Title = "PrismaPicker | Create New";
            return View();
        }

/* Scaffolded create method commented out. It works to send glass to Db, but I never got to the bottom of the issue I was having with 
 * non-unique primary key values being assigned to new entries.
 * 
 *        // POST method sends Glass object to the database
 *        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
 *        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
 *        [HttpPost]
 *        [ValidateAntiForgeryToken]
 *        public async Task<IActionResult> Create([Bind("Id,Sku,Name,Color,Link")] Glass glass)
 *        {
 *          if (ModelState.IsValid)
 *          {
 *            _context.Add(glass);
 *            await _context.SaveChangesAsync();
 *            return RedirectToAction(nameof(Index));
 *          }
 *          return View(glass);
 *      }
 */

        // GET method displays /Glasses/Edit/[ID]
        public async Task<IActionResult> Edit(int? id)
        {
            // Null check throws 404 if ID isn't valid
            if (id == null)
            {
                return NotFound();
            }

            // Get Glass from ID
            var glass = await _context.Glass.FindAsync(id);

            // Null check throws 404 if Glass isn't valid
            if (glass == null)
            {
                return NotFound();
            }

            ViewBag.Title = "PrismaPicker | Edit (" + glass.Id + ")";
            return View(glass);
        }

        // POST method sends form information back to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sku,Name,Color,Link")] Glass glass)
        {
            if (id != glass.Id)
            {
                return NotFound();
            }

            // Validate Information then try to push it out to DB
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlassExists(glass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Send user back to /Glass/Index
                return RedirectToAction(nameof(Index));
            }

            // Keep user on this view (happens if the Glass being sent isn't valid)
            return View(glass);
        }

        // GET method displays view Glasses/Delete/[ID]
        // Does same sorts of null checking as the prior GET methods have
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var glass = await _context.Glass.FirstOrDefaultAsync(m => m.Id == id);

            if (glass == null)
            {
                return NotFound();
            }

            ViewBag.Title = "PrismaPicker | Delete (" + glass.Id + ")";
            return View(glass);
        }

        // POST method uses ActionName annotation to avoid potential issues with methods having the same name and getting mad about overloading. Deletes entry.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Grab Glass by ID
            var glass = await _context.Glass.FindAsync(id);
            
            // Kill it and persist to db
            _context.Glass.Remove(glass);
            await _context.SaveChangesAsync();

            // Send user back to index
            return RedirectToAction(nameof(Index));
        }

        // Scaffolded method checks if Glass of this ID exists in db context
        private bool GlassExists(int id)
        {
            return _context.Glass.Any(e => e.Id == id);
        }
    }
}
