using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareJobApplicationv2.Data;
using CareJobApplicationv2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CareJobApplicationv2.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class ApplicantBrofilesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ApplicantBrofilesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ApplicantBrofiles
        public async Task<IActionResult> Index()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = CUserId;
            var UserProfiles = await _context.ApplicantBrofile.Where(u => u.UserId == CUserId).ToListAsync();
            return View(UserProfiles);
        }
        public IActionResult userProfile()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = CUserId;




            //ViewBag.EducationDegree = _context.EducationDegree.OrderBy(x => x.EducationDegreeTitle).ToList();
            // ViewBag.EducationMigor = _context.EducationMajor.OrderBy(x => x.EducationMajorTitle).ToList();

            // ViewBag.userProfile = _context.ApplicantBrofile.SingleOrDefault(u => u.UserId == CUserId);
            // ViewBag.userEducation = _context.ApplicantEducations.Where(u => u.UserId == CUserId).ToList();
            // ViewBag.userLanguage = _context.ApplicantLanguageSkilles.Where(u => u.UserId == CUserId).ToList();
            // ViewBag.userDocuments = _context.ApplicantDocuments.Where(u => u.UserId == CUserId).ToList();
            //ViewBag.userReferee = _context.applicantReferees.Where(u => u.UserId == CUserId).ToList();
            // ViewBag.userQ1Answers = _context.ApplicantQ1Answers.Where(u => u.UserId == CUserId).ToList();
            // ViewBag.userQ2Answers = _context.ApplicantQ2Answers.Where(u => u.UserId == CUserId).ToList();

            var ApplicantBrofile = _context.ApplicantBrofile.FirstOrDefault(u => u.UserId == CUserId);




            return View(ApplicantBrofile);
        }

        public IActionResult GetUserProfile()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = CUserId;
            ViewBag.ProfileId = 0;
            var userProfile = _context.ApplicantBrofile.SingleOrDefault(u => u.UserId == CUserId);
            if (userProfile == null)
                return View(new ApplicantBrofile());
            // else
            // {
            //     ViewBag.ProfileId = userProfile.Id;
            // }


            return View(userProfile);

        }

        // GET: ApplicantBrofiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplicantBrofile == null)
            {
                return NotFound();
            }

            var applicantBrofile = await _context.ApplicantBrofile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantBrofile == null)
            {
                return NotFound();
            }

            return View(applicantBrofile);
        }

        // GET: ApplicantBrofiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantBrofiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullNameEn,FullNameAr,BirthDate,GenderId,Nationality,MobileNumber,ApplicantPhoto,Address,Country,City,UserId")] ApplicantBrofile applicantBrofile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantBrofile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicantBrofile);
        }

        // GET: ApplicantBrofiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplicantBrofile == null)
            {
                return NotFound();
            }

            var applicantBrofile = await _context.ApplicantBrofile.FindAsync(id);
            if (applicantBrofile == null)
            {
                return NotFound();
            }
            return View(applicantBrofile);
        }

        // POST: ApplicantBrofiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullNameEn,FullNameAr,BirthDate,GenderId,Nationality,MobileNumber,ApplicantPhoto,Address,Country,City,UserId")] ApplicantBrofile applicantBrofile)
        {
            if (id != applicantBrofile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantBrofile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantBrofileExists(applicantBrofile.Id))
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
            return View(applicantBrofile);
        }

        // GET: ApplicantBrofiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicantBrofile == null)
            {
                return NotFound();
            }

            var applicantBrofile = await _context.ApplicantBrofile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantBrofile == null)
            {
                return NotFound();
            }

            return View(applicantBrofile);
        }

        // POST: ApplicantBrofiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicantBrofile == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplicantBrofile'  is null.");
            }
            var applicantBrofile = await _context.ApplicantBrofile.FindAsync(id);
            if (applicantBrofile != null)
            {
                _context.ApplicantBrofile.Remove(applicantBrofile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantBrofileExists(int id)
        {
            return _context.ApplicantBrofile.Any(e => e.Id == id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FullNameEn,FullNameAr,BirthDate,GenderId,Nationality,MobileNumber,ApplicantPhoto,Address,Country,City,UserId")] ApplicantBrofile applicantBrofile)
        public async Task<IActionResult> ProfileCreate(ApplicantBrofile applicantBrofile)
        {


            if (ModelState.IsValid)
            {
                if (applicantBrofile.UserId == null)
                {
                    if (_userManager.GetUserId(HttpContext.User) != null)
                    {
                        applicantBrofile.UserId = _userManager.GetUserId(HttpContext.User);
                    }
                    UploadProfilePhoto(applicantBrofile);

                    //applicantBrofile.UserId = User.Identity.
                    _context.Add(applicantBrofile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(userProfile));
                }
                else
                {
                    UploadProfilePhoto(applicantBrofile);

                    //applicantBrofile.UserId = User.Identity.
                    _context.Update(applicantBrofile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(userProfile));
                }
                


            }
            //applicantBrofile
            return View();
            //return PartialView("_ApplicantProfile", applicantBrofile);
        }
        /************************************************************************/
        private void UploadProfilePhoto(ApplicantBrofile applicantBrofile)
        {
            var CurrentFullName = applicantBrofile.FullNameEn;
            var BrofileImageFullName = CurrentFullName.Replace(' ', '-');
            var ProfileImage = HttpContext.Request.Form.Files;
            if (ProfileImage.Count() > 0)
            {
                //upload Profile Image
                string ImageFileName = Guid.NewGuid().ToString() + BrofileImageFullName + Path.GetExtension(ProfileImage[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images/ProfilePhotos/", ImageFileName), FileMode.Create);
                ProfileImage[0].CopyTo(fileStream);

                applicantBrofile.ApplicantPhoto = ImageFileName;


            }
        }

        [HttpGet]
        public JsonResult GetApplicantEducations()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            var applicantEducation = _context.ApplicantEducations.Where(u => u.UserId == CUserId).ToList();

            return new JsonResult(Ok(applicantEducation));
        }


        /**********************************  Education ********************************/
        [HttpGet]
        public IActionResult GetApplicantEducation()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            //var applicantEducation = _context.ApplicantEducations.Where(u => u.UserId == CUserId).ToList();
            var applicantEducation = (from res in _context.ApplicantEducations
                                      where (res.UserId == CUserId)
                                      select new
                                      {
                                          res.Id,
                                          Degree= res.EducationDegree.EducationDegreeTitle,
                                             Major = res.EducationMajor.EducationMajorTitle,
                                             res.CountryEducation,
                                             res.InstitutionEn,
                                             res.InstitutionAr,
                                             status= res.EducationStatus.StatusTitleEn,
                                             res.YearOfStarting,
                                             res.YearOfFinished
                                });

            var recordsTotal = applicantEducation.Count();

            var jsonData = new { recordsFiltered = recordsTotal, data = applicantEducation };
            //var jsonData = new { data = applicantLanguages };

            return Ok(applicantEducation);
        }

        [HttpGet]
        public ActionResult AddEditApplicantEducation(int Id = 0)
        {
            
            return View(new ApplicantEducations());
        }
        [HttpPost]
        public ActionResult AddEditApplicantEducation()
        {

            return View(new ApplicantEducations());
        }

        // GET: ApplicantEducations/Create
        [HttpGet]
        public ActionResult AddApplicantEducation()
        {
            ViewData["EducationDegreeId"] = new SelectList(_context.EducationDegree, "Id", "EducationDegreeTitle");

            ViewData["EducationMajorId"] = new SelectList(_context.EducationMajor, "Id", "EducationMajorTitle");

            ViewData["EducationCountries"] = new SelectList(_context.Countries, "Iso", "CountryName");
            ViewBag.TestCountry = _context.Countries.Where(i => i.Iso == "YE").Select(n => n.CountryName);
            ViewBag.ListCount = _context.Countries.ToList().Count;
            // ViewBag.ListCount2 = _context.Countries.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddApplicantEducation(ApplicantEducations applicantEducations)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                applicantEducations.UserId = CUserId;
                _context.Add(applicantEducations);
                await _context.SaveChangesAsync();
                status = true;
                message = "Saved Successfully";
                //return RedirectToAction(nameof(Index));
                return Json(new { success = status, message = message });
            }
            ViewData["EducationDegreeId"] = new SelectList(_context.EducationDegree, "Id", "EducationDegreeTitle", applicantEducations.EducationDegreeId);
            ViewData["EducationDegreeId"] = new SelectList(_context.EducationMajor, "Id", "EducationMajorTitle", applicantEducations.EducationDegreeId);
            ViewData["EducationStatus"] = new SelectList(_context.EducationStatus, "Id", "StatusTitleEn", applicantEducations.Status);
            //return View(applicantEducations);
            //return Json(new { success = status, message = message });
            return View();
        }
        public IActionResult EditApplicantEducation(int? id)
        {
            if (id == null || _context.ApplicantEducations == null)
            {
                return NotFound();
            }

            var applicantEducations = _context.ApplicantEducations.Find(id);
            if (applicantEducations == null)
            {
                return NotFound();
            }
            ViewData["EducationDegreeId"] = new SelectList(_context.EducationDegree, "Id", "EducationDegreeTitle", applicantEducations.EducationDegreeId);
            ViewData["EducationMajorId"] = new SelectList(_context.EducationMajor, "Id", "EducationMajorTitle", applicantEducations.EducationMajorId);
            ViewData["EducationStatus"] = new SelectList(_context.EducationStatus, "Id", "StatusTitleEn", applicantEducations.Status);


            ViewData["EducationCountries"] = new SelectList(_context.Countries, "Iso", "CountryName", applicantEducations.CountryEducation);

            
            return View(applicantEducations);
        }

        // POST: ApplicantEducations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApplicantEducation(int id, ApplicantEducations applicantEducations)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (id != applicantEducations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicantEducations.UserId = CUserId;
                    _context.Update(applicantEducations);
                    await _context.SaveChangesAsync();
                    status = true;
                    message = "Saved Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantEducationsExists(applicantEducations.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
                return Json(new { success = status, message = message });
            }
            ViewData["EducationDegreeId"] = new SelectList(_context.EducationDegree, "Id", "EducationDegreeTitle", applicantEducations.EducationDegreeId);
            ViewData["EducationMajorId"] = new SelectList(_context.EducationMajor, "Id", "EducationMajorTitle", applicantEducations.EducationMajorId);
            ViewData["EducationStatus"] = new SelectList(_context.EducationStatus, "Id", "StatusTitleEn", applicantEducations.Status);


            ViewData["EducationCountries"] = new SelectList(_context.Countries, "Iso", "CountryName", applicantEducations.CountryEducation);
            return View();
        }
        public async Task<IActionResult> DeleteApplicantEducation(int? id)
        {
            var status = false;
            if (id == null || _context.ApplicantEducations == null)
            {
                //return NotFound();
                status = false;
            }

            var applicantEducations = _context.ApplicantEducations.Find(id);
            if (applicantEducations != null)
            {
                _context.ApplicantEducations.Remove(applicantEducations);
                await _context.SaveChangesAsync();
                status = true;
            }

            return Json(status);

        }

        private bool ApplicantEducationsExists(int id)
        {
            return _context.ApplicantEducations.Any(e => e.Id == id);
        }
        /**********************************  Language ********************************/
        [HttpGet]
        public IActionResult GetApplicantLanguages()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            //var applicantLanguages = _context.ApplicantLanguageSkilles.Where(u => u.UserId == CUserId).ToList();

            var applicantLanguages = (from res in _context.ApplicantLanguageSkilles
                                      where (res.UserId == CUserId)
                                      select new
                                      {
                                          res.Id,
                                          res.LanguageName,
                                          res.SpeakingLevels,
                                          SpeakingSkill = res.SpeakingLevels.LanguageLevelEn,
                                          SpeakingCs = res.SpeakingLevels.CssClass,
                                          res.ReadingLevels,
                                          ReadingSkill = res.ReadingLevels.LanguageLevelEn,
                                          ReadingSkillCss = res.ReadingLevels.CssClass,
                                          res.WritingLevels,
                                          WritingSkill = res.WritingLevels.LanguageLevelEn,
                                          WritingSkillCss = res.WritingLevels.CssClass,

                                      });

            var recordsTotal = applicantLanguages.Count();

            var jsonData = new { recordsFiltered = recordsTotal, data = applicantLanguages };
            //var jsonData = new { data = applicantLanguages };

            return Ok(applicantLanguages);
        }
        /*
        [HttpGet]
        public IActionResult UpdateApplicantLanguage(int? id=0)
        {
            if (id == null || _context.ApplicantEducations == null)
            {
                return NotFound();
            }

            var applicantLanguageSkilles = _context.ApplicantLanguageSkilles.Find(id);
            if (applicantLanguageSkilles == null)
            {
                return NotFound();
            }
            ViewData["SpeakingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.SpeakingLevel);
            ViewData["ReadingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.ReadingLevel);
            ViewData["WritingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.WritingLevel);


            ViewData["LanguageName"] = new SelectList(_context.languages, "language_name", "language_name", applicantLanguageSkilles.LanguageName);


            return View(applicantLanguageSkilles);
            
        }

            [HttpPost]
        public IActionResult UpdateApplicantLanguage(ApplicantLanguageSkilles languageSkilles)
        {
            var status = false;
            var message = "";
            if (ModelState.IsValid)
            {
                if (languageSkilles.Id == null)
                {
                    if (_userManager.GetUserId(HttpContext.User) != null)
                    {
                        languageSkilles.UserId = _userManager.GetUserId(HttpContext.User);
                    }

                    _context.Add(languageSkilles);
                    _context.SaveChangesAsync();
                    status = true;
                    // return RedirectToAction(nameof(userProfile));
                }
                else
                {

                    _context.Update(languageSkilles);
                    _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(userProfile));
                    status = true;
                }
                

            }
            return Json(new { success = status, message = message });
            //var jsonData = new { status = status };
            //return PartialView("_ApplicantLanguages", languageSkilles);
            //return Ok(jsonData);
            //return Json(status);
            return View();

        }
        */


        [HttpGet]
        public ActionResult AddApplicantLanguage()
        {
            ViewData["SpeakingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn");
            ViewData["ReadingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn");
            ViewData["WritingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn");
            ViewData["LanguageName"] = new SelectList(_context.languages, "language_name", "language_name");

            // ViewBag.ListCount2 = _context.Countries.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddApplicantLanguage(ApplicantLanguageSkilles applicantLanguageSkilles)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                applicantLanguageSkilles.UserId = CUserId;
                _context.Add(applicantLanguageSkilles);
                await _context.SaveChangesAsync();
                status = true;
                message = "Saved Successfully";
                //return RedirectToAction(nameof(Index));
                return Json(new { success = status, message = message });
            }
            ViewData["SpeakingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.SpeakingLevel);
            ViewData["ReadingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.ReadingLevel);
            ViewData["WritingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.WritingLevel);


            ViewData["LanguageName"] = new SelectList(_context.languages, "language_name", "language_name", applicantLanguageSkilles.LanguageName);

            return View();
        }
        public IActionResult EditApplicantLanguage(int? id)
        {
            if (id == null || _context.ApplicantLanguageSkilles == null)
            {
                return NotFound();
            }

            var applicantLanguageSkilles = _context.ApplicantLanguageSkilles.Find(id);
            if (applicantLanguageSkilles == null)
            {
                return NotFound();
            }
            ViewData["SpeakingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.SpeakingLevel);
            ViewData["ReadingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.ReadingLevel);
            ViewData["WritingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.WritingLevel);


            ViewData["LanguageName"] = new SelectList(_context.languages, "language_name", "language_name", applicantLanguageSkilles.LanguageName);


            return View(applicantLanguageSkilles);
        }

        // POST: ApplicantEducations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApplicantLanguage(int id, ApplicantLanguageSkilles applicantLanguageSkilles)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (id != applicantLanguageSkilles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicantLanguageSkilles.UserId = CUserId;
                    _context.Update(applicantLanguageSkilles);
                    await _context.SaveChangesAsync();
                    status = true;
                    message = "Saved Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantLanguageSkillesExists(applicantLanguageSkilles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // return RedirectToAction(nameof(Index));
                return Json(new { success = status, message = message });
            }
            
            ViewData["SpeakingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.SpeakingLevel);
            ViewData["ReadingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.ReadingLevel);
            ViewData["WritingLevel"] = new SelectList(_context.LanguageLevel, "Id", "LanguageLevelEn", applicantLanguageSkilles.WritingLevel);


            ViewData["LanguageName"] = new SelectList(_context.languages, "language_name", "language_name", applicantLanguageSkilles.LanguageName);

            return View();
        }

        public async Task<IActionResult> DeleteApplicantLanguage(int? id)
        {
            var status = false;
            if (id == null || _context.ApplicantLanguageSkilles == null)
            {
                //return NotFound();
                status = false;
            }

            var applicantLanguageSkilles = _context.ApplicantLanguageSkilles.Find(id);
            if (applicantLanguageSkilles != null)
            {
                _context.ApplicantLanguageSkilles.Remove(applicantLanguageSkilles);
                await _context.SaveChangesAsync();
                status = true;
            }

            return Json(status);

        }

        private bool ApplicantLanguageSkillesExists(int id)
        {
            return _context.ApplicantLanguageSkilles.Any(e => e.Id == id);
        }
        /********************************** Computer Skills ****************************/

        // GET: ApplicantComputerSkills
        public IActionResult GetApplicantComputerSkills()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            // var applicationComputerSkills = _context.ApplicantComputerSkills.Include(a => a.ComputerSkillLevels).Include(a => a.ComputerSkills).Where(a=>a.UserId == userId);
            // return View(await applicationComputerSkills.ToListAsync());
            var applicationComputerSkills = (from res in _context.ApplicantComputerSkills
                                             where (res.UserId == CUserId)
                                      select new
                                      {
                                          Id = res.Id,
                                          ComputerSkill = res.ComputerSkills.SkillTitleEnglish,
                                          Skill = res.ComputerSkillLevels.LevelTitleEnglish,
                                          SkillValue = res.ComputerSkillLevels.LevelScore
                                          

                                      });

            var recordsTotal = applicationComputerSkills.Count();
            var jsonData = new { recordsFiltered = recordsTotal, data = applicationComputerSkills };
            //var jsonData = new { data = applicantLanguages };

            return Ok(applicationComputerSkills);
        }
        public IActionResult AddApplicantComputerSkill()
        {
            ViewData["SkillLevelId"] = new SelectList(_context.Set<ComputerSkillLevels>(), "Id", "LevelTitleEnglish", "Select Subject");
            ViewData["SkillId"] = new SelectList(_context.Set<ComputerSkills>(), "Id", "SkillTitleEnglish", "Select Subject");
            return PartialView("_AddApplicantComputerSkills");
        }

        // POST: ApplicantComputerSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddApplicantComputerSkill([Bind("Id,SkillId,SkillLevelId,UserId")] ApplicantComputerSkills applicantComputerSkills)
        {
            var status = false;
            var message = "Something went wrong";
            if (ModelState.IsValid)
            {
                applicantComputerSkills.UserId = _userManager.GetUserId(HttpContext.User);
                _context.Add(applicantComputerSkills);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                status = true;
                message = "Saved Successfully";
                
            }
            //ViewData["SkillLevelId"] = new SelectList(_context.Set<ComputerSkillLevels>(), "Id", "LevelTitleEnglish", applicantComputerSkills.SkillLevelId);
            //ViewData["SkillId"] = new SelectList(_context.Set<ComputerSkills>(), "Id", "SkillTitleEnglish", applicantComputerSkills.SkillId);

            //applicantComputerSkills
            // return View();
            return Json(new { success = status, message = message });
        }
        // GET: ApplicantComputerSkills/Edit/5
        public IActionResult EditApplicantComputerSkill(int? id)
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (id == null || _context.ApplicantComputerSkills == null)
            {
                return NotFound();
            }

            var applicantComputerSkills = _context.ApplicantComputerSkills.Find(id);
            if (applicantComputerSkills == null)
            {
                return NotFound();
            }
            ViewData["SkillLevelId"] = new SelectList(_context.Set<ComputerSkillLevels>(), "Id", "LevelTitleEnglish", applicantComputerSkills.SkillLevelId);
            ViewData["SkillId"] = new SelectList(_context.Set<ComputerSkills>(), "Id", "SkillTitleEnglish", applicantComputerSkills.SkillId);
            //return View(applicantComputerSkills);
            return PartialView("_EditApplicantComputerSkills");
        }

        // POST: ApplicantComputerSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApplicantComputerSkill(int id, [Bind("Id,SkillId,SkillLevelId,UserId")] ApplicantComputerSkills applicantComputerSkills)
        {
            var status = false;
            var message = "Something went wrong";
            if (id != applicantComputerSkills.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicantComputerSkills.UserId = _userManager.GetUserId(HttpContext.User);
                    _context.Update(applicantComputerSkills);
                    await _context.SaveChangesAsync();
                    status = true;
                    message = "Saved Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantComputerSkillsExists(applicantComputerSkills.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
            }
            //ViewData["SkillLevelId"] = new SelectList(_context.Set<ComputerSkillLevels>(), "Id", "LevelTitleEnglish", applicantComputerSkills.SkillLevelId);
            //ViewData["SkillId"] = new SelectList(_context.Set<ComputerSkills>(), "Id", "SkillTitleEnglish", applicantComputerSkills.SkillId);
            //return View(applicantComputerSkills);
            return Json(new { success = status, message = message });
        }

       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteApplicantComputerSkill(int? id)
        {
            var status = false;
            if (id == null || _context.ApplicantComputerSkills == null)
            {
                //return NotFound();
                status = false;
            }

            var applicantComputerSkills = _context.ApplicantComputerSkills.Find(id);
            if (applicantComputerSkills != null)
            {
                _context.ApplicantComputerSkills.Remove(applicantComputerSkills);
                await _context.SaveChangesAsync();
                status = true;
            }

            return Json(status);

        }

        private bool ApplicantComputerSkillsExists(int id)
        {
            return _context.ApplicantComputerSkills.Any(e => e.Id == id);
        }

        /**********************************  Experience ********************************/
        public IActionResult GetApplicantExperiences()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            var applicantExperiences = _context.ApplicantExperiences.Where(u => u.UserId == CUserId);

            var recordsTotal = applicantExperiences.Count();

            var jsonData = new { recordsFiltered = recordsTotal, data = applicantExperiences };

            return Ok(applicantExperiences);
        }
        [HttpGet]
        public IActionResult CreateApplicantExperiences()
        {
            return View();
        }

        // POST: ApplicantReferees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplicantExperiences(ApplicantExperiences applicantExperiences)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                applicantExperiences.UserId = CUserId;
                _context.Add(applicantExperiences);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                status = true;
                message = "Saved Successfully";
                
            }
            //return View();
            return Json(new { success = status, message = message });
        }

        // GET: ApplicantReferees/Edit/5
        public IActionResult EditApplicantExperiences(int? id)
        {
            if (id == null || _context.ApplicantExperiences == null)
            {
                return NotFound();
            }

            var applicantExperiences = _context.ApplicantExperiences.Find(id);
            if (applicantExperiences == null)
            {
                return NotFound();
            }
            return View(applicantExperiences);
        }

        // POST: ApplicantReferees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> EditApplicantExperiences(int id, ApplicantExperiences applicantExperiences)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (id != applicantExperiences.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicantExperiences.UserId = CUserId;
                    _context.Update(applicantExperiences);
                    await _context.SaveChangesAsync();
                    status = true;
                    message = "Saved Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExperienceExists(applicantExperiences.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                
            }
            //return View();
            return Json(new { success = status, message = message });
        }


        //JsonResult
        public async Task<IActionResult> DeleteApplicantExperience(int? id)
        {
            var status = false;
            if (id == null || _context.ApplicantExperiences == null)
            {
                //return NotFound();
                status = false;
            }

            var applicantExperiences = _context.ApplicantExperiences.Find(id);
            if (applicantExperiences != null)
            {
                _context.ApplicantExperiences.Remove(applicantExperiences);
                await _context.SaveChangesAsync();
                status = true;
            }

            return Json(status);

        }

        private bool ApplicantExperienceExists(int id)
        {
            return _context.ApplicantExperiences.Any(e => e.Id == id);
        }
        /**********************************  Referee ********************************/
        [HttpGet]
        public IActionResult GetApplicantReferee()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            var applicantReferees = _context.ApplicantReferee.Where(u => u.UserId == CUserId).ToList();
            

            var recordsTotal = applicantReferees.Count();

            var jsonData = new { recordsFiltered = recordsTotal, data = applicantReferees };
            //var jsonData = new { data = applicantLanguages };

            return Ok(applicantReferees);
        }
        // GET: ApplicantReferees/Create
        public IActionResult CreateApplicantReferee()
        {
            return View();
        }

        // POST: ApplicantReferees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplicantReferee(ApplicantReferee applicantReferee)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                applicantReferee.UserId = CUserId;
                _context.Add(applicantReferee);
                 await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                status = true;
                message = "Saved Successfully";
                return Json(new { success = status, message = message });
            }
            return View();
        }
        // GET: ApplicantReferees/Edit/5
        public IActionResult EditApplicantReferee(int? id)
        {
            if (id == null || _context.ApplicantReferee == null)
            {
                return NotFound();
            }

            var applicantReferee = _context.ApplicantReferee.Find(id);
            if (applicantReferee == null)
            {
                return NotFound();
            }
            return View(applicantReferee);
        }

        // POST: ApplicantReferees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApplicantReferee(int id, ApplicantReferee applicantReferee)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (id != applicantReferee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicantReferee.UserId = CUserId;
                    _context.Update(applicantReferee);
                    await _context.SaveChangesAsync();
                    status = true;
                    message = "Saved Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantRefereeExists(applicantReferee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return Json(new { success = status, message = message });
            }
            return View();
        }

        // GET: ApplicantReferees/Delete/5
        
        public async Task<IActionResult> DeleteApplicantReferee(int? id)
        {
            var status = false;
            if (id == null || _context.ApplicantReferee == null)
            {
                //return NotFound();
                status = false;
            }

            var applicantReferee = _context.ApplicantReferee.Find(id);
            if (applicantReferee != null)
            {
                _context.ApplicantReferee.Remove(applicantReferee);
                await _context.SaveChangesAsync();
                status = true;
            }

            return Json(status);

        }
        private bool ApplicantRefereeExists(int id)
        {
            return _context.ApplicantReferee.Any(e => e.Id == id);
        }


        /************* Docs ****************/
        public IActionResult GetApplicantDocs()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            var applicantDocuments = _context.ApplicantDocuments.Where(u => u.UserId == CUserId).ToList();
           /* var applicantDocuments = (from res in _context.ApplicantDocuments
                                      where (res.UserId == CUserId)
                                      select new
                                      {
                                          res.Id,
                                          res.Document,
                                          documentType = res.DocumentType,
                                          DocumentTypeEn = res.DocumentTypes.DocumentTypeEn,
                                          DocumentTypeAr = res.DocumentTypes.DocumentTypeAr

                                      });
           */

            var recordsTotal = applicantDocuments.Count();

            var jsonData = new { recordsFiltered = recordsTotal, data = applicantDocuments };
            
            //var jsonData = new { data = applicantLanguages };

            return Ok(applicantDocuments);

           // return View(_context.ApplicantDocuments.ToList());
        }



        /**********************************  Q1 Answers ********************************/
        [HttpGet]
        public IActionResult GetApplicantQ1()
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            var applicantQ1Answers = _context.ApplicantQ1Answers.Where(u => u.UserId == CUserId).ToList();


            var recordsTotal = applicantQ1Answers.Count();

            var jsonData = new { recordsFiltered = recordsTotal, data = applicantQ1Answers };
            //var jsonData = new { data = applicantLanguages };

            return Ok(applicantQ1Answers);
        }
        // GET: ApplicantReferees/Create
        public IActionResult AddApplicantQ1()
        {
            return View();
        }

        // POST: ApplicantReferees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddApplicantQ1(ApplicantQ1Answers applicantQ1Answers)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                applicantQ1Answers.UserId = CUserId;
                _context.Add(applicantQ1Answers);
                _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                status = true;
                message = "Saved Successfully";
                return Json(new { success = status, message = message });
            }
            return View();
        }
        // GET: ApplicantReferees/Edit/5
        public IActionResult EditApplicantQ1(int? id)
        {
            if (id == null || _context.ApplicantReferee == null)
            {
                return NotFound();
            }

            var applicantQ1Answers = _context.ApplicantQ1Answers.Find(id);
            if (applicantQ1Answers == null)
            {
                return NotFound();
            }
            return View(applicantQ1Answers);
        }

        // POST: ApplicantReferees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditApplicantQ1(int id, ApplicantQ1Answers applicantQ1Answers)
        {
            var status = false;
            var message = "";
            var CUserId = _userManager.GetUserId(HttpContext.User);
            if (id != applicantQ1Answers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicantQ1Answers.UserId = CUserId;
                    _context.Update(applicantQ1Answers);
                    _context.SaveChangesAsync();
                    status = true;
                    message = "Saved Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantQ1AnswersExists(applicantQ1Answers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Json(new { success = status, message = message });
            }
            return View();
        }

        // GET: ApplicantReferees/Delete/5

        public JsonResult DeleteApplicantQ1(int? id)
        {
            var status = false;
            if (id == null || _context.ApplicantQ1Answers == null)
            {
                //return NotFound();
                status = false;
            }

            var applicantQ1Answers = _context.ApplicantQ1Answers.Find(id);
            if (applicantQ1Answers != null)
            {
                _context.ApplicantQ1Answers.Remove(applicantQ1Answers);
                _context.SaveChanges();
                status = true;
            }

            return Json(status);

        }
        private bool ApplicantQ1AnswersExists(int id)
        {
            return _context.ApplicantReferee.Any(e => e.Id == id);
        }

    }
}
