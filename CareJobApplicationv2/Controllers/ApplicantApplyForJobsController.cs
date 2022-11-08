using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareJobApplicationv2.Data;
using CareJobApplicationv2.Models;
using Microsoft.AspNetCore.Identity;

namespace CareJobApplicationv2.Controllers
{
    
    public class ApplicantApplyForJobsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicantApplyForJobsController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // GET: ApplicantApplyForJobs
        public IActionResult Index()
        {
            var applicantApplyForJob = _context.ApplicantApplyForJob.Where(a=>a.JobId == 1);
            return View(applicantApplyForJob.ToList());
        }

        // GET: ApplicantApplyForJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplicantApplyForJob == null)
            {
                return NotFound();
            }

            var applicantApplyForJob = await _context.ApplicantApplyForJob
                .Include(a => a.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantApplyForJob == null)
            {
                return NotFound();
            }

            return View(applicantApplyForJob);
        }

        // GET: ApplicantApplyForJobs/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Id");
            return View();
        }

        // POST: ApplicantApplyForJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplyDate,JobId,UserId,JobFormData,IsLongList,IsInterViewList")] ApplicantApplyForJob applicantApplyForJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantApplyForJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Id", applicantApplyForJob.JobId);
            return View(applicantApplyForJob);
        }

        // GET: ApplicantApplyForJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplicantApplyForJob == null)
            {
                return NotFound();
            }

            var applicantApplyForJob = await _context.ApplicantApplyForJob.FindAsync(id);
            if (applicantApplyForJob == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Id", applicantApplyForJob.JobId);
            return View(applicantApplyForJob);
        }

        // POST: ApplicantApplyForJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplyDate,JobId,UserId,JobFormData,IsLongList,IsInterViewList")] ApplicantApplyForJob applicantApplyForJob)
        {
            if (id != applicantApplyForJob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantApplyForJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantApplyForJobExists(applicantApplyForJob.Id))
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
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Id", applicantApplyForJob.JobId);
            return View(applicantApplyForJob);
        }

        // GET: ApplicantApplyForJobs/Delete/5
        public async Task<IActionResult> Delete(int? JobId)
        {   var userId = _userManager.GetUserId(HttpContext.User);
            if (JobId == null || _context.ApplicantApplyForJob == null)
            {
                return NotFound();
            }
            var getId = await _context.ApplicantApplyForJob.Where(a=>a.UserId== userId && a.JobId == JobId).SingleOrDefaultAsync();
            var applicantApplyForJob = await _context.ApplicantApplyForJob
                .Include(a => a.Job)
                .FirstOrDefaultAsync(m => m.Id == getId.Id);
            if (applicantApplyForJob == null)
            {
                return NotFound();
            }

            return View(applicantApplyForJob);
        }

        // POST: ApplicantApplyForJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicantApplyForJob == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplicantApplyForJob'  is null.");
            }
            var applicantApplyForJob = await _context.ApplicantApplyForJob.FindAsync(id);
            if (applicantApplyForJob != null)
            {
                _context.ApplicantApplyForJob.Remove(applicantApplyForJob);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantApplyForJobExists(int id)
        {
          return _context.ApplicantApplyForJob.Any(e => e.Id == id);
        }
    }
}
