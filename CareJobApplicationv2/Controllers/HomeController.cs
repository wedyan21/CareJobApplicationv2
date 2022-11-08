using CareJobApplicationv2.Data;
using CareJobApplicationv2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CareJobApplicationv2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public JsonResult GetOpenPositions()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            ViewBag.JobCategoriesId = new SelectList(_context.Categories, "CategoryNameEn", "CategoryNameEn", "Select Category");
            ViewBag.JobLocationList = new SelectList(_context.Branchs, "BranchTitleEn", "BranchTitleEn", "Select Location");
            ViewBag.JobStatus = new SelectList(_context.JobStatus, "JobStatuse", "JobStatuse", "Select Status");
            // var applicantEducation = _context.ApplicantEducations.Where(u => u.UserId == CUserId).ToList();

            if (CUserId != null)
            {
                ViewBag.CUser = CUserId;
                var ListJobs2 = (
                                  from j in _context.Jobs
                                  where (j.Deadline > DateTime.Now)
                                  orderby j.PublishDate descending
                                  orderby j.Deadline descending
                                  select new
                                  {
                                      Id = j.Id,
                                      JobTitle = j.JobTitle,
                                      PublishDate = j.PublishDate,
                                      Deadline = j.Deadline,
                                      Location = j.Branchs.BranchTitleEn,
                                      Category = j.Categories.CategoryNameEn,
                                      Buser = new
                                      {
                                          cuser = (
                                                      from a in _context.ApplicantApplyForJob
                                                      where (a.JobId == j.Id)
                                                      where (a.UserId == CUserId)

                                                      select a.UserId
                                                      ).FirstOrDefault()

                                      }
                                  }

                               );

                //ViewData["mysql2"] = ListJobs2.ToQueryString();

                ViewData["JobAppliedLList"] = ListJobs2.Count();

                ViewData["JobAppliedLListToList"] = ListJobs2.ToList();
                return new JsonResult(Ok(ListJobs2.ToList()));
            }

            var OpenPosions = _context.Jobs
                                       .Include(j => j.Branchs)
                                       .Include(j => j.Categories)
                                       .Include(j => j.JobStatus)

                                       .Where(j => j.Deadline > DateTime.Now).OrderByDescending(j => j.Deadline).ToList();

            return new JsonResult(Ok(OpenPosions));
        }

        public async Task<IActionResult> Index()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            ViewData["JobAppliedLListToList"] = "";
            ViewBag.JobCategoriesId = new SelectList(_context.Categories, "CategoryNameEn", "CategoryNameEn", "Select Category");
            ViewBag.JobLocationList = new SelectList(_context.Branchs, "BranchTitleEn", "BranchTitleEn", "Select Location");
            //ViewBag.JobStatus = new SelectList(_context.JobStatus, "JobStatuse", "JobStatuse", "Select Status");

            var applicationDbContext = _context.Jobs
                                       .Include(j => j.Branchs)
                                       .Include(j => j.Categories)
                                       .Include(j => j.JobStatus)

                                       .Where(j => j.Deadline > DateTime.Now).OrderByDescending(j => j.Deadline);



            if (CUserId != null)
            {
                ViewBag.CUser = CUserId;
                var ListJobs2 = (
                                  from j in _context.Jobs
                                  where (j.Deadline > DateTime.Now)
                                  orderby j.PublishDate descending
                                  orderby j.Deadline descending
                                  select new
                                  {
                                      j.Id,
                                      j.JobTitle,
                                      j.PublishDate,
                                      j.Deadline,
                                      j.Branchs.BranchTitleEn,
                                      j.Categories.CategoryNameEn,
                                      Buser = new
                                      {
                                          cuser = (
                                                      from a in _context.ApplicantApplyForJob
                                                      where (a.JobId == j.Id)
                                                      where (a.UserId == CUserId)

                                                      select a.UserId
                                                      ).FirstOrDefault()

                                      }
                                  }

                               );

                //ViewData["mysql2"] = ListJobs2.ToQueryString();

                ViewData["JobAppliedLList"] = ListJobs2.Count();

                ViewData["JobAppliedLListToList"] = ListJobs2.ToList();

            }




            return View(await applicationDbContext.ToListAsync());
            //return View(await applicationDbContext.ToList());
        }

        public async Task<IActionResult> ClosedJobs()
        {
            ViewBag.JobCategoriesId = new SelectList(_context.Categories, "CategoryNameEn", "CategoryNameEn", "Select Category");
            ViewBag.JobLocationList = new SelectList(_context.Branchs, "BranchTitleEn", "BranchTitleEn", "Select Location");
            //ViewBag.JobStatus = new SelectList(_context.JobStatus, "JobStatuse", "JobStatuse", "Select Status");

            var applicationDbContext = _context.Jobs.Include(j => j.Branchs).Include(j => j.Categories).Include(j => j.JobStatus).Where(j => j.Deadline < DateTime.Now).OrderByDescending(j => j.Deadline);
            return View(await applicationDbContext.ToListAsync());
            //return View();
        }
        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .Include(j => j.Branchs)
                .Include(j => j.Categories)
                .Include(j => j.JobStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize]
        public ActionResult Apply(int JobId)
        {
            // var JobTitle = _context.Jobs.Where(d => d.StatusId == 1).Where(d => d.Id == JobId).Select(d => d.JobTitle);
            var Job = _context.Jobs.Where(d => d.Id == JobId).FirstOrDefault();
            ViewBag.JobId = JobId;

            ViewBag.JobTitleEn = Job.JobTitle;
            ViewBag.JobFormData = Job.JobForm;
            ViewBag.DeadLine = Job.Deadline;

            ViewBag.JobData = Job;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplicantApplyForJob applicantApplyForJob)
        {
            ViewBag.ResultType = null;
            var UserId = _userManager.GetUserId(HttpContext.User);
            var Check = _context.ApplicantApplyForJob.Where(a => a.JobId == applicantApplyForJob.JobId && a.UserId == UserId);

            if (Check.Count() < 1)
            {
                var CurrentApply = new ApplicantApplyForJob();
                CurrentApply.JobId = applicantApplyForJob.JobId;
                CurrentApply.UserId = UserId;
                CurrentApply.ApplyDate = DateTime.Now;
                CurrentApply.JobFormData = applicantApplyForJob.JobFormData;

                _context.ApplicantApplyForJob.Add(CurrentApply);
                _context.SaveChanges();
                ViewBag.ResultType = "success";
                ViewBag.Result = "Applyed Success";
            }
            else
            {
                ViewBag.ResultType = "warning";
                ViewBag.Result = "You Alredy Applyed to this Job";
            }


            return View();
        }

        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var MyJobs = _context.ApplicantApplyForJob.Where(a => a.UserId == userId);
            return View(MyJobs.ToList());
        }
        [Authorize]
        public ActionResult GetJobDetailsByUser(int Id)
        {
            var JobByUser = _context.ApplicantApplyForJob.Find(Id);
            if (JobByUser == null)
            {
                //return HttpNotFound();
            }
            return View(JobByUser);
        }
        [Authorize]
        [HttpGet]

        public async Task<IActionResult> EditeApply(int? JobId)
        {
            // var JobTitle = _context.Jobs.Where(d => d.StatusId == 1).Where(d => d.Id == JobId).Select(d => d.JobTitle);
            var userId = _userManager.GetUserId(HttpContext.User);
            // var Job = _context.Jobs.Where(d => d.Id == JobId).FirstOrDefault();
            //var ApplyJob = _context.ApplicantApplyForJob.Where(a => a.JobId == JobId && a.UserId == userId).FirstOrDefault();

            var getId = await _context.ApplicantApplyForJob.Where(a => a.UserId == userId && a.JobId == JobId).SingleOrDefaultAsync();
            var applicantApplyForJob = await _context.ApplicantApplyForJob
                .Include(a => a.Job)
                .FirstOrDefaultAsync(m => m.Id == getId.Id);

            ViewBag.JobId = JobId;

            ViewBag.JobTitleEn = applicantApplyForJob.Job.JobTitle;
            ViewBag.JobFormData = applicantApplyForJob.JobFormData;
            ViewBag.DeadLine = applicantApplyForJob.Job.Deadline;

            ViewBag.JobData = applicantApplyForJob.Job;

            return View(applicantApplyForJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditeApply(int id, ApplicantApplyForJob applicantApplyForJob)
        {
            ViewBag.ResultType = null;
            if (id != applicantApplyForJob.Id)
            {
                return NotFound();
            }
            var UserId = _userManager.GetUserId(HttpContext.User);
            // var Check = _context.ApplicantApplyForJob.Where(a => a.Id == applicantApplyForJob.Id && a.UserId == UserId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                try
                {
                    applicantApplyForJob.UserId = UserId;
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
            /*
            if (Check != null)
            {
                
                applicantApplyForJob.UserId = UserId;
                //applicantApplyForJob.Id = Check.Id;
                
                _context.ApplicantApplyForJob.Update(applicantApplyForJob);
                _context.SaveChanges();
                                

                ViewBag.ResultType = "success";
                ViewBag.Result = "Applyed Success";
            }
            else
            {
                ViewBag.ResultType = "warning";
                ViewBag.Result = "You did not Apply to this Job yet!";
            }
            */

            return View();
        }

        [Authorize]
        [HttpGet]
        //GET
        // public async Task<IActionResult> Withdraw(int? JobId)
        public async Task<IActionResult> Withdraw(int? JobId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (JobId == null || _context.ApplicantApplyForJob == null)
            {
                return NotFound();
            }

            // var applicantApplyForJob =  _context.ApplicantApplyForJob.FirstOrDefaultAsync(a => a.JobId == JobId && a.UserId == CUserId);

            var getId = await _context.ApplicantApplyForJob.Where(a => a.UserId == userId && a.JobId == JobId).SingleOrDefaultAsync();
            var applicantApplyForJob = await _context.ApplicantApplyForJob
                .Include(a => a.Job)
                .FirstOrDefaultAsync(m => m.Id == getId.Id);

            if (JobId == null || applicantApplyForJob == null)
            {
                return NotFound(applicantApplyForJob);
            }



            return View(applicantApplyForJob);
        }
        // POST: 
        [HttpPost, ActionName("Withdraw")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdrawfirmed(int id)
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