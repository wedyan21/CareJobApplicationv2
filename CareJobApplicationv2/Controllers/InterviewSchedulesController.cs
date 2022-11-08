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
    public class InterviewSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        

        public InterviewSchedulesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: InterviewSchedules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InterviewSchedule.Include(i => i.Job).Include(p=>p.InterviewPanels).Include(c=>c.interviewCandidates);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InterviewSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InterviewSchedule == null)
            {
                return NotFound();
            }

            var interviewSchedule = await _context.InterviewSchedule
                .Include(i => i.Job)
                .Include(p => p.InterviewPanels)
                .Include(c => c.interviewCandidates)
                .FirstOrDefaultAsync(m => m.Id == id);

            /*
            var interviewSchedule2 = _context.InterviewSchedule( i=> new
            {
                JobTitle = interviewSchedule.JobTitle,
                InterviewLocation = interviewSchedule.InterviewLocation,
                InterviewDay = interviewSchedule.InterviewDay,
                InterviewDate = interviewSchedule.InterviewDate,
                InterviewTime = interviewSchedule.InterviewTime,

                InterviewCandidates = i.InterviewCandidates.Select(i => new
                
            }
                
                )
            */
            var interviewSchedule2 = from m in _context.InterviewSchedule.Where(m => m.Id == id)
                                     select new
                                     {
                                         JobTitle = m.Job.JobTitle,
                                         InterviewLocation = m.Location,
                                         InterviewDay = m.InterviewDay,
                                         InterviewDate = m.InterviewDate,
                                         InterviewTime = m.InterviewTime,
                                         InterviewPanels = m.InterviewPanels
                                        



        };

            var InterviewCandidates = (from c in _context.InterviewCandidates.Where(m => m.InterviewScheduleId == id)
                                       join ab in _context.ApplicantBrofile on c.CandidateId equals ab.UserId

                                       select new
                                       {
                                           UserId = ab.UserId,
                                           CandidateName = ab.FullNameEn,
                                           Mobile = ab.MobileNumber,

                                           InterViewDate = c.InterViewDate,
                                           InterViewTime = c.InterviewTime,
                                           coments = c.Comments

                                       });
        var InterviewCandidates2 = (from v in _context.ApplicantApplyForJob.Where(v=> v.IsInterViewList == true && v.JobId == 1 )
                                         join c in _context.InterviewCandidates.Where(m => m.InterviewScheduleId == id) on v.UserId equals c.CandidateId
                                         join ab in _context.ApplicantBrofile on v.UserId equals ab.UserId

                                                               select new
                                                               {
                                                                   id=c.Id,
                                                                   UserId = ab.UserId,
                                                                   CandidateName = ab.FullNameEn,
                                                                   Mobile = ab.MobileNumber,
                                                                   Email = ab.Email,
                                                                   InterViewDate = c.InterViewDate,
                                                                   InterViewTime = c.InterviewTime,
                                                                   coments = c.Comments,
                                                                   Comment2 = v.FinalAnalysisComment

                                                               });

            ViewData["InterviewCandidates"] = InterviewCandidates2.ToQueryString();
            ViewData["InterviewCandidates2"] = InterviewCandidates2.ToList();

            //var cJobId = _context.InterviewSchedule.Where(i=> i.Id == id).Select(i => i.JobId).FirstOrDefault();
            // var candidates = _context.ApplicantApplyForJob.Where(a=> a.JobId == cJobId & a.IsInterViewList == true )
            //     .Join(ApplicantBrofile )
            /*    Include(a=> a.applicantBrofile).Select(a=> new
            {
               name =  a.applicantBrofile.FullNameEn,
               email = a.applicantBrofile.ApplicationUser.Email,
               //InterViewDate = a.interviewCandidates.InterViewDate,
               //InterViewTime = a.interviewCandidates.InterviewTime,
               //coments = a.interviewCandidates.Comments

            }).ToList();
            */

             ViewData["candidates"] = interviewSchedule2.ToList();
            if (interviewSchedule == null)
            {
                return NotFound();
            }

            return View(interviewSchedule);
        }

        // GET: InterviewSchedules/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "JobTitle");
            return View();
        }

        // POST: InterviewSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobId,Location,InterviewDate,InterviewDay,InterviewTime")] InterviewSchedule interviewSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interviewSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "JobTitle", interviewSchedule.JobId);
            return View(interviewSchedule);
        }

        // GET: InterviewSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InterviewSchedule == null)
            {
                return NotFound();
            }

            var interviewSchedule = await _context.InterviewSchedule.FindAsync(id);
            if (interviewSchedule == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "JobTitle", interviewSchedule.JobId);
            return View(interviewSchedule);
        }

        // POST: InterviewSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobId,Location,InterviewDate,InterviewDay,InterviewTime")] InterviewSchedule interviewSchedule)
        {
            if (id != interviewSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interviewSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewScheduleExists(interviewSchedule.Id))
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
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "JobTitle", interviewSchedule.JobId);
            return View(interviewSchedule);
        }

        // GET: InterviewSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InterviewSchedule == null)
            {
                return NotFound();
            }

            var interviewSchedule = await _context.InterviewSchedule
                .Include(i => i.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewSchedule == null)
            {
                return NotFound();
            }

            return View(interviewSchedule);
        }

        // POST: InterviewSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InterviewSchedule == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InterviewSchedule'  is null.");
            }
            var interviewSchedule = await _context.InterviewSchedule.FindAsync(id);
            if (interviewSchedule != null)
            {
                _context.InterviewSchedule.Remove(interviewSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewScheduleExists(int id)
        {
          return _context.InterviewSchedule.Any(e => e.Id == id);
        }
    }
}
