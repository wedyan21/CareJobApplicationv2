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
    public class InterviewCandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterviewCandidatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InterviewCandidates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InterviewCandidates.Include(i => i.InterviewSchedule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InterviewCandidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InterviewCandidates == null)
            {
                return NotFound();
            }

            var interviewCandidates = await _context.InterviewCandidates
                .Include(i => i.InterviewSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewCandidates == null)
            {
                return NotFound();
            }

            return View(interviewCandidates);
        }

        // GET: InterviewCandidates/Create
        public IActionResult Create()
        {
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id");
            return View();
        }

        // POST: InterviewCandidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobId,InterviewScheduleId,CandidateId,InterViewDate,InterviewTime,Comments")] InterviewCandidates interviewCandidates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interviewCandidates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id", interviewCandidates.InterviewScheduleId);
            return View(interviewCandidates);
        }

        // GET: InterviewCandidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InterviewCandidates == null)
            {
                return NotFound();
            }

            var interviewCandidates = await _context.InterviewCandidates.FindAsync(id);
            if (interviewCandidates == null)
            {
                return NotFound();
            }
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id", interviewCandidates.InterviewScheduleId);
            return View(interviewCandidates);
        }

        // POST: InterviewCandidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobId,InterviewScheduleId,CandidateId,InterViewDate,InterviewTime,Comments")] InterviewCandidates interviewCandidates)
        {
            if (id != interviewCandidates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interviewCandidates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewCandidatesExists(interviewCandidates.Id))
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
            ViewData["InterviewScheduleId"] = new SelectList(_context.InterviewSchedule, "Id", "Id", interviewCandidates.InterviewScheduleId);
            return View(interviewCandidates);
        }

        // GET: InterviewCandidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InterviewCandidates == null)
            {
                return NotFound();
            }

            var interviewCandidates = await _context.InterviewCandidates
                .Include(i => i.InterviewSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewCandidates == null)
            {
                return NotFound();
            }

            return View(interviewCandidates);
        }

        // POST: InterviewCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InterviewCandidates == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InterviewCandidates'  is null.");
            }
            var interviewCandidates = await _context.InterviewCandidates.FindAsync(id);
            if (interviewCandidates != null)
            {
                _context.InterviewCandidates.Remove(interviewCandidates);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewCandidatesExists(int id)
        {
          return _context.InterviewCandidates.Any(e => e.Id == id);
        }
    }
}
