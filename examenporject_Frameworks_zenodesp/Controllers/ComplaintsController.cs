using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using examenporject_Frameworks_zenodesp.Data;
using examenporject_Frameworks_zenodesp.Models;
using System.Net.Sockets;

namespace examenporject_Frameworks_zenodesp.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Complaints
        public async Task<IActionResult> Index(string SelectMode = "A")
        {
            
            List<modeItem> modeItems = new List<modeItem>
            {
                new modeItem { value = "A", text = "All Complaints" },
                new modeItem { value = "S", text = "Sent By You" },
            };
            ComplaintIndexViewModel viewmodel = new ComplaintIndexViewModel();
            viewmodel.SelectMode = SelectMode;
            viewmodel.Modes = new SelectList(modeItems, "value", "text", SelectMode);

            if (SelectMode == "A")
            {
                viewmodel.Complaints = _context.Complaints.Where(c => c.deleted > DateTime.Now).ToList();
            }
            else
            {
                viewmodel.Complaints = _context.Complaints.Where(c => c.deleted > DateTime.Now && c.complaintLogger.UserName == User.Identity.Name).ToList();
            }

            return View(viewmodel);

            //return _context.Complaints != null ? 
            //              View(await _context.Complaints.Where(c => c.deleted > DateTime.Now).ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Complaints'  is null.");
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            // TypeList to check input
            var TypeList = _context.ComplaintType.Where(t => t.deleted > DateTime.Now).OrderBy(t => t.TypeName);

            ViewData["ComplaintTypeId"] = new SelectList(_context.ComplaintType.Where(t => t.deleted > DateTime.Now).OrderBy(t => t.TypeName), "Id", "TypeName");

            
            return View(new Complaint
            {
                EmployeeId = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id,
            });
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,EmployeeId,ComplaintTypeId,created,deleted")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                complaint.complaintLogger = await _context.Users.FindAsync(complaint.EmployeeId);
                complaint.ComplaintType = await _context.ComplaintType.FindAsync(complaint.ComplaintTypeId);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,EmployeeId,ComplaintTypeId,created,deleted")] Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Complaints == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Complaints'  is null.");
            }
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                complaint.deleted = DateTime.Now;
                _context.Complaints.Update(@complaint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
          return (_context.Complaints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
