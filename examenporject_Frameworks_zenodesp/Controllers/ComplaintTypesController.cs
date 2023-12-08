using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using examenporject_Frameworks_zenodesp.Data;
using examenporject_Frameworks_zenodesp.Models;

namespace examenporject_Frameworks_zenodesp.Controllers
{
    public class ComplaintTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComplaintTypes
        public async Task<IActionResult> Index()
        {
              return _context.ComplaintType != null ? 
                          View(await _context.ComplaintType.Where(ct => ct.deleted > DateTime.Now).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ComplaintType'  is null.");
        }

        // GET: ComplaintTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ComplaintType == null)
            {
                return NotFound();
            }

            var complaintType = await _context.ComplaintType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }

            return View(complaintType);
        }

        // GET: ComplaintTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComplaintTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] ComplaintType complaintType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintType);
        }

        // GET: ComplaintTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ComplaintType == null)
            {
                return NotFound();
            }

            var complaintType = await _context.ComplaintType.FindAsync(id);
            if (complaintType == null)
            {
                return NotFound();
            }
            return View(complaintType);
        }

        // POST: ComplaintTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,TypeName")] ComplaintType complaintType)
        {
            if (id != complaintType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintTypeExists(complaintType.Id))
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
            return View(complaintType);
        }

        // GET: ComplaintTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ComplaintType == null)
            {
                return NotFound();
            }

            var complaintType = await _context.ComplaintType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }

            return View(complaintType);
        }

        // POST: ComplaintTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ComplaintType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ComplaintType'  is null.");
            }
            var complaintType = await _context.ComplaintType.FindAsync(id);
            if (complaintType != null)
            {
               complaintType.deleted = DateTime.Now;
                _context.ComplaintType.Update(complaintType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintTypeExists(string id)
        {
          return (_context.ComplaintType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
