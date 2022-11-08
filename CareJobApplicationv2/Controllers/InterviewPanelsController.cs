using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareJobApplicationv2.Data;
using CareJobApplicationv2.Models;

namespace CareJobApplicationv2.Controllers
{
    public class InterviewPanelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterviewPanelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InterviewPanels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InterviewPanel.Include(i => i.InterviewSchedule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InterviewPanels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InterviewPanel == null)
            {
                return NotFound();
            }

            var interviewPanel = await _context.InterviewPanel
                .Include(i => i.InterviewSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewPanel == null)
            {
                return NotFound();
            }

            return View(interviewPanel);
        }

        // GET: InterviewPanels/Create
        public IActionResult Create()
        {
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id");
            return View();
        }

        // POST: InterviewPanels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InterviewScheduleId,Name,Position")] InterviewPanel interviewPanel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interviewPanel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id", interviewPanel.InterviewScheduleId);
            return View(interviewPanel);
        }

        // GET: InterviewPanels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InterviewPanel == null)
            {
                return NotFound();
            }

            var interviewPanel = await _context.InterviewPanel.FindAsync(id);
            if (interviewPanel == null)
            {
                return NotFound();
            }
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id", interviewPanel.InterviewScheduleId);
            return View(interviewPanel);
        }

        // POST: InterviewPanels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InterviewScheduleId,Name,Position")] InterviewPanel interviewPanel)
        {
            if (id != interviewPanel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interviewPanel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewPanelExists(interviewPanel.Id))
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
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id", interviewPanel.InterviewScheduleId);
            return View(interviewPanel);
        }

        // GET: InterviewPanels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InterviewPanel == null)
            {
                return NotFound();
            }

            var interviewPanel = await _context.InterviewPanel
                .Include(i => i.InterviewSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewPanel == null)
            {
                return NotFound();
            }

            return View(interviewPanel);
        }

        // POST: InterviewPanels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InterviewPanel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InterviewPanel'  is null.");
            }
            var interviewPanel = await _context.InterviewPanel.FindAsync(id);
            if (interviewPanel != null)
            {
                _context.InterviewPanel.Remove(interviewPanel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewPanelExists(int id)
        {
          return _context.InterviewPanel.Any(e => e.Id == id);
        }
    }
}
