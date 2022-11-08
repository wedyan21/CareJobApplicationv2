using CareJobApplicationv2.Data;
using CareJobApplicationv2.Models;
using CareJobApplicationv2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web;

namespace CareJobApplicationv2.Controllers
{
    [Authorize(Roles = "HR-Manager, HRBranches")]
    public class CandidatesManagementController : Controller
    {
        private readonly ILogger<CandidatesManagementController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        

        


        public CandidatesManagementController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<CandidatesManagementController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }


        public IActionResult Index()
        {
            var canditates = _context.Jobs.Select(j => new
            {
                j.Id,
                Position = j.JobTitle,
                Category = j.Categories.CategoryNameEn,
                Branch = j.Branchs.BranchTitleEn,
                PublishDate = j.PublishDate,
                Deadline = j.Deadline,
                Status = j.JobStatus.JobStatuse,
                StatusColor = j.JobStatus.StatusColor,
                CountApplicants = j.ApplyForJobs.Where(a => a.JobId == j.Id).Count(),
                CountCandidates = j.ApplyForJobs.Where(l=>l.IsLongList ==true).Count(),
                CountShortlist = j.CandidatesEvaluators.Where(jo=>jo.JobId == j.Id).Select(i=>i.EvaluationProccess).FirstOrDefault(),
                CountInterViewList = j.ApplyForJobs.Where(i=> i.IsInterViewList ==true).Count(),
                HasWinner = j.ApplyForJobs.Where(i => i.IsInterViewList == true).Select(w=>w.UserId).FirstOrDefault()
            }).ToList();


            var JobsByBranch = _context.Branchs.OrderBy(b => b.BranchTitleEn)
                                .Select(b => new
                                {
                                    b.Id,
                                    Branch = b.BranchTitleEn,
                                    CountJobs = b.Jobs.Count()

                                }).ToList();

            var JobsByCategory = _context.Categories.OrderBy(c => c.CategoryNameEn)
                .Select(c => new
                {
                    c.Id,
                    Category = c.CategoryNameEn,
                    CountJobs = c.Jobs.Count()
                }).ToList();

            ViewData["JobStatuse"] = _context.JobStatus.OrderBy(c => c.JobStatuse).Select(s => s.Jobs).Count();

            ViewData["JobsByBranchList"] = JobsByBranch;
            ViewData["JobsByCategoryList"] = JobsByCategory;


            var can = _context.Jobs.Select(j => new
            {
                j.Id,
                Position = j.JobTitle,
                Category = j.Categories.CategoryNameEn,
                Branch = j.Branchs.BranchTitleEn,
                PublishDate = j.PublishDate,
                Deadline = j.Deadline,
                Status = j.JobStatus.JobStatuse,
                StatusColor = j.JobStatus.StatusColor,
                CountApplicants = j.ApplyForJobs.Where(a=>a.JobId == j.Id).Count(),
                CountCandidates = j.ApplyForJobs.Where(l => l.IsLongList == true).Count(),
                CountShortlist = j.CandidatesEvaluators.Where(jo => jo.JobId == j.Id).Select(i => i.EvaluationProccess).FirstOrDefault(),
                CountInterViewList = j.ApplyForJobs.Where(i => i.IsInterViewList == true).Count(),
                HasWinner = j.ApplyForJobs.Where(i => i.IsInterViewList == true).Select(w => w.UserId).FirstOrDefault()
            });

            //ViewData["can"] = can;


            // ViewData["CandidatesByJob"] = CandidatesByJob;
            return View(canditates);
        }

        [HttpGet]
        public IActionResult JobCandidates(int? JobId)
        {
            //JobId = 1;
            // if(JobId != 0)
            // {
            ViewData["JobId"] = JobId;
            // ViewBag.GenderType = new SelectList(_context.GenderTypes, "Gender", "Gender");
            ViewBag.EducationDegree = new SelectList(_context.EducationDegree.OrderBy(e => e.EducationDegreeTitle), "EducationDegreeTitle", "EducationDegreeTitle");
            ViewBag.EducationMajor = new SelectList(_context.EducationMajor.OrderBy(m => m.EducationMajorTitle), "EducationMajorTitle", "EducationMajorTitle");
            ViewBag.ExperienceRelated = new SelectList(_context.ExperiencePeriod.OrderBy(e => e.ExperiencePeriodText), "ExperiencePeriodText", "ExperiencePeriodText");
            ViewBag.Languages = new SelectList(_context.languages.OrderBy(e => e.language_name), "language_name", "language_name");
            ViewBag.LanguageSkills = new SelectList(_context.LanguageLevel.OrderBy(l => l.LanguageLevelEn), "Id", "LanguageLevelEn");


            var query = from aj in _context.ApplicantApplyForJob.Where(u => u.JobId == JobId )

                        join ab in _context.ApplicantBrofile on aj.UserId equals ab.UserId
                        //join ce in _context.ApplicantEducations on aj.UserId equals ce.UserId
                        select new
                        {
                            aj.JobId,
                            aj.JobFormData,
                            aj.IsLongList,
                            ab.FullNameEn,
                            ab.GenderId,
                            ab.UserId,
                            Education = (from ce in _context.ApplicantEducations
                                         where ce.UserId == aj.UserId
                                         select ce).Distinct().ToList(),
                            EducationDegree = (from ce in _context.ApplicantEducations
                                               where ce.UserId == aj.UserId
                                               select new
                                               {
                                                   Degree = ce.EducationDegree.EducationDegreeTitle,
                                                   Score = ce.EducationDegree.Score
                                                   // Major = ce.EducationMajor.EducationMajorTitle
                                               }).Distinct().ToList(),
                            EducationMajor = (from ce in _context.ApplicantEducations
                                              where ce.UserId == aj.UserId
                                              select new
                                              {
                                                  //Degree = ce.EducationDegree.EducationDegreeTitle,
                                                  Major = ce.EducationMajor.EducationMajorTitle
                                              }).Distinct().ToList(),
                            Languages = (from l in _context.ApplicantLanguageSkilles
                                         where l.UserId == aj.UserId
                                         select l).Distinct().ToList(),

                            ExperienceList = (from ex in _context.ApplicantExperiences
                                              where ex.UserId == aj.UserId
                                              select ex).Distinct().ToList(),



                        };

            /*
                            //where aj.JobId == 1
                            //group new {aj, ab, ce } by aj.UserId into Cand
                            //from ce in CandidateEducations.DefaultIfEmpty()
                        select new {
                           
                                    aj.UserId, Ub=ab.UserId,// Uc = ce.UserId,
                                    aj.JobId,  
                                    ab.FullNameEn,
                                    ab.GenderId,
                               //    MajorId = ce.EducationMajorId,
                               //     Major = ce.EducationMajor.EducationMajorTitle.ToList(),
                              DegreeId = ce.EducationDegreeId,
                             Degree = ce.EducationDegree.EducationDegreeTitle
                           
                       };
            */

            var Candidtates2 = _context.ApplicantApplyForJob.Where(aj => aj.JobId == 1)
                   .Select(c => new
                   {
                       c.Id,
                       c.JobId,
                       c.UserId,
                       c.JobFormData,

                   });

            var Candidtates = _context.ApplicantApplyForJob.Where(aj => aj.JobId == JobId)
                    .Select(c => new
                    {
                        c.Id,
                        c.JobId,
                        c.UserId,
                        c.JobFormData,
                        candidateBrofile = _context.ApplicantBrofile.Where(a => a.UserId == c.UserId).Select(a => new
                        {
                            a.Id,
                            a.FullNameEn,
                            a.FullNameAr,
                            a.GenderId,
                            a.BirthDate,
                            a.Nationality,
                            a.MobileNumber,
                            a.ApplicantPhoto,
                            a.Address,
                            a.Country,
                            a.City

                        }).ToList(),
                        CandidateEducation = _context.ApplicantEducations.Where(e => e.UserId == c.UserId).Select(e => new
                        {
                            e.Id,
                            e.EducationDegreeId,
                            Degree = e.EducationDegree.EducationDegreeTitle,
                            e.EducationMajorId,
                            Major = e.EducationMajor.EducationMajorTitle

                        }).ToList(),

                    });


            ViewData["mysql2"] = query.ToString();
            ViewData["mysql1"] = query.Count();
            /**************************************************    Main Taible *******************************/
            ViewData["mysql3"] = query.ToList();
            
            
            ViewData["mysql4"] = query.ToString();


            var t = (from ce in _context.ApplicantEducations
                     select new
                     {
                         Degree = ce.EducationDegree.EducationDegreeTitle,

                     }).Distinct().ToList();

            ViewData["t"] = t;
            // ViewBag.
            //  }
           

            return View(Candidtates2.ToList());
        }

        [Authorize(Roles = "HR-Manager, HRBranches, Evaluator")]
        [HttpGet]
        public IActionResult JobCandidateScores(int? JobId)
        {
            int cJobId = (int)JobId;
            ViewData["JobId"] = JobId;
            ViewData["DataEvaluation"] = null;
            var CUserId = _userManager.GetUserId(HttpContext.User);
            bool IsNewProccess = true;
            if(EvaluationByEvaluatorExists(cJobId, CUserId))
            {
                var EvaluationData = getDataOfEvaluationProcess(cJobId, CUserId);
                //var EvaluationData = _context.CandidatesEvaluators.Where(x => x.JobId == cJobId && x.EvaluatorId == CUserId).FirstOrDefault();
                ViewData["DataEvaluation"] = EvaluationData;
                IsNewProccess = false;


            }
            ViewBag.IsNewProccess = IsNewProccess;
            var query = from aj in _context.ApplicantApplyForJob.Where(u => u.JobId == JobId && u.IsLongList == true)

                        join ab in _context.ApplicantBrofile on aj.UserId equals ab.UserId
                        //join ce in _context.ApplicantEducations on aj.UserId equals ce.UserId
                        select new
                        {
                            aj.JobId,
                            aj.JobFormData,
                            ab.FullNameEn,
                            ab.GenderId,
                            ab.UserId,
                            Education = (from ce in _context.ApplicantEducations
                                         where ce.UserId == aj.UserId
                                         select ce).Distinct().ToList(),
                            EducationDegree = (from ce in _context.ApplicantEducations
                                               where ce.UserId == aj.UserId
                                               select new
                                               {
                                                   Degree = ce.EducationDegree.EducationDegreeTitle,
                                                   Score = ce.EducationDegree.Score
                                                   // Major = ce.EducationMajor.EducationMajorTitle
                                               }).Distinct().ToList(),
                            EducationMajor = (from ce in _context.ApplicantEducations
                                              where ce.UserId == aj.UserId
                                              select new
                                              {
                                                  //Degree = ce.EducationDegree.EducationDegreeTitle,
                                                  Major = ce.EducationMajor.EducationMajorTitle
                                              }).Distinct().ToList(),
                            Languages = (from l in _context.ApplicantLanguageSkilles
                                         where l.UserId == aj.UserId
                                         select l).Distinct().ToList(),

                            ExperienceList = (from ex in _context.ApplicantExperiences
                                              where ex.UserId == aj.UserId
                                              select ex).Distinct().ToList(),



                        };



            var FormTHDT = _context.Jobs.Where(j => j.Id == JobId).Select(j => j.JobForm).FirstOrDefault();
            // string sql = FormTHDT.ToString();
            if (FormTHDT != null)
            {
                dynamic dynJson = JsonConvert.DeserializeObject(FormTHDT);


                ViewData["FTHDT"] = dynJson;
            }
            else
            {
                ViewData["FTHDT"] = "";
            }




            return View(query.ToList());
        }


        [HttpGet]
        public IActionResult FinalJobCandidateAnalysis(int? JobId)
        {
            int cJobId = (int)JobId;
            ViewData["JobId"] = JobId;
            ViewData["DataEvaluation"] = null;
            var CUserId = _userManager.GetUserId(HttpContext.User);
            bool IsNewProccess = true;
            if (EvaluationByEvaluatorExists(cJobId, CUserId))
            {
                var EvaluationData = getDataOfAllEvaluationProcess(cJobId);
                //var EvaluationData = _context.CandidatesEvaluators.Where(x => x.JobId == cJobId && x.EvaluatorId == CUserId).FirstOrDefault();
                ViewData["DataEvaluation"] = EvaluationData;
                IsNewProccess = false;


            }
            ViewBag.IsNewProccess = IsNewProccess;
            var query = from aj in _context.ApplicantApplyForJob.Where(u => u.JobId == JobId && u.IsLongList == true)

                        join ab in _context.ApplicantBrofile on aj.UserId equals ab.UserId
                        //join ce in _context.ApplicantEducations on aj.UserId equals ce.UserId
                        select new
                        {
                            aj.JobId,
                            aj.JobFormData,
                            ab.FullNameEn,
                            ab.GenderId,
                            ab.UserId,
                            Education = (from ce in _context.ApplicantEducations
                                         where ce.UserId == aj.UserId
                                         select ce).Distinct().ToList(),
                            EducationDegree = (from ce in _context.ApplicantEducations
                                               where ce.UserId == aj.UserId
                                               select new
                                               {
                                                   Degree = ce.EducationDegree.EducationDegreeTitle,
                                                   Score = ce.EducationDegree.Score
                                                   // Major = ce.EducationMajor.EducationMajorTitle
                                               }).Distinct().ToList(),
                            EducationMajor = (from ce in _context.ApplicantEducations
                                              where ce.UserId == aj.UserId
                                              select new
                                              {
                                                  //Degree = ce.EducationDegree.EducationDegreeTitle,
                                                  Major = ce.EducationMajor.EducationMajorTitle
                                              }).Distinct().ToList(),
                            Languages = (from l in _context.ApplicantLanguageSkilles
                                         where l.UserId == aj.UserId
                                         select l).Distinct().ToList(),

                            ExperienceList = (from ex in _context.ApplicantExperiences
                                              where ex.UserId == aj.UserId
                                              select ex).Distinct().ToList(),



                        };



            var FormTHDT = _context.Jobs.Where(j => j.Id == JobId).Select(j => j.JobForm).FirstOrDefault();
            // string sql = FormTHDT.ToString();
            if (FormTHDT != null)
            {
                dynamic dynJson = JsonConvert.DeserializeObject(FormTHDT);


                ViewData["FTHDT"] = dynJson;
            }
            else
            {
                ViewData["FTHDT"] = "";
            }




            return View(query.ToList());
        }

        [HttpGet]
        public IActionResult InterViewList(int? JobId)
        {
            
           // if (JobId != 0)
           // {
                var query = from aj in _context.ApplicantApplyForJob.Where(u => u.JobId == JobId && u.IsInterViewList == true)
                            join ab in _context.ApplicantBrofile on aj.UserId equals ab.UserId 
                            //into ajab
                            //from iv in _context.ApplicantInterView.Where(iu => iu.CandidateId == aj.UserId)
                            // join  iv in _context.ApplicantInterView on aj.UserId equals iv.CandidateId 

                             

                            select new
                            {
                                aj.JobId,
                                aj.JobFormData,
                                ab.FullNameEn,
                                ab.GenderId,
                                ab.MobileNumber,
                                ab.UserId,
                              

                            };
           // }
           ViewBag.QT= query.ToString();


            return View(query.ToList());
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveInterviewDate([Bind("Id,JobId,CandidateId,InterViewDate,Comments")] InterviewCandidates interviewCandidates)
        {
            var status = false;
            var message = "";
            if (ModelState.IsValid)
            {
                _context.Add(interviewCandidates);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                status = true;
                message = "Saved Successfully";
            }
            //return View(applicantInterView);
            return Json(new { success = status, message = message });
        }

        [HttpPost]
        
        
        [HttpGet]
        public JsonResult ScoresDatatable(int? JobId)
        {
            //JobId = 1;
            // if(JobId != 0)
            // {
            ViewData["JobId"] = JobId;



            var query = from aj in _context.ApplicantApplyForJob.Where(u => u.JobId == JobId)

                        join ab in _context.ApplicantBrofile on aj.UserId equals ab.UserId
                        //join ce in _context.ApplicantEducations on aj.UserId equals ce.UserId
                        select new
                        {
                            aj.JobId,
                            aj.JobFormData,
                            ab.FullNameEn,
                            ab.GenderId,
                            ab.UserId,
                            Education = (from ce in _context.ApplicantEducations
                                         where ce.UserId == aj.UserId
                                         select ce).Distinct().ToList(),
                            EducationDegree = (from ce in _context.ApplicantEducations
                                               where ce.UserId == aj.UserId
                                               select new
                                               {
                                                   Degree = ce.EducationDegree.EducationDegreeTitle,
                                                   Score = ce.EducationDegree.Score
                                                   // Major = ce.EducationMajor.EducationMajorTitle
                                               }).Distinct().ToList(),
                            EducationMajor = (from ce in _context.ApplicantEducations
                                              where ce.UserId == aj.UserId
                                              select new
                                              {
                                                  //Degree = ce.EducationDegree.EducationDegreeTitle,
                                                  Major = ce.EducationMajor.EducationMajorTitle
                                              }).Distinct().ToList(),
                            Languages = (from l in _context.ApplicantLanguageSkilles
                                         where l.UserId == aj.UserId
                                         select l).Distinct().ToList(),

                            ExperienceList = (from ex in _context.ApplicantExperiences
                                              where ex.UserId == aj.UserId
                                              select ex).Distinct().ToList(),



                        };







            return Json(query.ToList());
        }

        [HttpPost]
        public JsonResult JobsByBranch()
        {

            var query = _context.Branchs.OrderBy(b => b.BranchTitleEn)
                               .Select(b => new
                               {
                                   b.Id,
                                   label = b.BranchTitleEn,
                                   y = b.Jobs.Count()

                               }).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("label", System.Type.GetType("System.String"));
            dt.Columns.Add("y", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();
            foreach (var i in query)
            {
                dr = dt.NewRow();
                dr["label"] = i.label;
                dr["y"] = i.y;
                dt.Rows.Add(dr);

            }

            List<object> iData = new List<object>();
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            //Source data returned as JSON  
            return Json(iData);
        }

        [HttpPost]
        public JsonResult JobsByCategory()
        {
            var query = _context.Categories.OrderBy(c => c.CategoryNameEn)
                .Select(c => new
                {
                    c.Id,
                    label = c.CategoryNameEn,
                    y = c.Jobs.Count()
                }).ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("label", System.Type.GetType("System.String"));
            dt.Columns.Add("y", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();
            foreach (var i in query)
            {
                dr = dt.NewRow();
                dr["label"] = i.label;
                dr["y"] = i.y;
                dt.Rows.Add(dr);

            }

            List<object> iData = new List<object>();
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            //Source data returned as JSON  
            return Json(iData);
        }

        [HttpPost]
        public JsonResult JobsByStatus()
        {
            var query = _context.JobStatus.OrderBy(c => c.JobStatuse)
                .Select(c => new
                {
                    c.Id,
                    label = c.JobStatuse,
                    y = c.Jobs.Count()
                }).ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("label", System.Type.GetType("System.String"));
            dt.Columns.Add("y", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();
            foreach (var i in query)
            {
                dr = dt.NewRow();
                dr["label"] = i.label;
                dr["y"] = i.y;
                dt.Rows.Add(dr);

            }

            List<object> iData = new List<object>();
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            //Source data returned as JSON  
            return Json(iData);
        }
        
        [HttpPost]
        public ActionResult CreateLongList(int JobId, string LongList)
        {
            //var l = LongList.Length;
            dynamic dynJson = JsonConvert.DeserializeObject(LongList);

            var applicants = _context.ApplicantApplyForJob.Where(x => x.JobId == JobId).ToList();
            foreach (var item in applicants)
            {
                item.IsLongList = false;
            }
            _context.SaveChanges();
            foreach (var item in applicants)
            {
                foreach(var s in dynJson)
                {
                    if (item.UserId == s.Value )
                    {
                        item.IsLongList = true;
                    }
                    
                }
               // item.AddToShortList = false;

            }
            _context.SaveChanges();
            return View();
        }


        [HttpPost]
        public ActionResult CreateInterViewList(int JobId, string InterViewList)
        {
            //var l = LongList.Length;
            dynamic dynJson = JsonConvert.DeserializeObject(InterViewList);

            var InterView = _context.ApplicantApplyForJob.Where(x => x.JobId == JobId).ToList();
            foreach (var item in InterView)
            {
                item.IsInterViewList = false;
            }
            _context.SaveChanges();
            foreach (var item in InterView)
            {
                foreach (var s in dynJson)
                {
                    if (item.UserId == s.Value)
                    {
                        item.IsInterViewList = true;
                    }

                }
                // item.AddToShortList = false;

            }
            _context.SaveChanges();
            return View();
        }

        [HttpPost]
        
         public async Task<IActionResult> ShortListByEvaluator(int JobId, string EvaluationProccess, CandidatesEvaluators candidatesEvaluators)
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            

            //, string EvaluationProccess
            int JobId2 = candidatesEvaluators.JobId;
            var status = false;
            var message = "";
            

            if (JobId2 != 0 )
            {
                candidatesEvaluators.EvaluatorId = CUserId;
                if (!EvaluationByEvaluatorExists(JobId2, CUserId))
               
                {
                    
                    _context.Add(candidatesEvaluators);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    candidatesEvaluators.Id = getIdOfEvaluationProcess(JobId2, CUserId);
                    
                    _context.Update(candidatesEvaluators);
                    await _context.SaveChangesAsync();
                    
                }
                 
                 
                
                status = true;
                message = "Saved Successfully";
                return Json(new { success = status, message = message });
            }

            return View();
        }

        private bool EvaluationByEvaluatorExists(int JobId, string userId)
        {
            return _context.CandidatesEvaluators.Any(e => e.JobId == JobId && e.EvaluatorId == userId);
        }
        private int getIdOfEvaluationProcess(int JobId, string userId)
        {
            int CId = _context.CandidatesEvaluators.Where(x => x.JobId == JobId && x.EvaluatorId == userId).Select(e => e.Id).FirstOrDefault();
            return CId;
        }
        private object getDataOfEvaluationProcess(int JobId, string userId)
        {
            object Data = _context.CandidatesEvaluators.Where(x => x.JobId == JobId && x.EvaluatorId == userId).FirstOrDefault();
            return Data;
        }
        private object getDataOfAllEvaluationProcess(int JobId)
        {
            object Data = _context.CandidatesEvaluators.Where(x => x.JobId == JobId ).ToList();
            return Data;
        }

        public ActionResult GetEvaluationDataByUser(int JobId)
        {
            var CUserId = _userManager.GetUserId(HttpContext.User);
            var query = _context.CandidatesEvaluators.Where(x => x.JobId == JobId && x.EvaluatorId == CUserId).FirstOrDefault();

            return Json(query);
        }


       // [HttpGet]
        public IActionResult AddFinalAnalysisComment(int JobId, string userId)
        {
            if (JobId == 0)
            {
                return NotFound();
            }
            if (userId == null )
            {
                return NotFound();
            }

            var CId = _context.ApplicantApplyForJob.Where(x => x.JobId == JobId && x.UserId == userId).Select(x => x.Id).FirstOrDefault();

            var query = _context.ApplicantApplyForJob.Where(x => x.JobId == JobId && x.UserId ==userId).Select(x => new {x.Id, x.JobId, x.UserId , x.FinalAnalysisComment}).FirstOrDefault();
            var viewModel = new FinalAnalysisComments
            {
                Id = query.Id,
                JobId = query.JobId,
                UserId = query.UserId,
                FinalAnalysisComment = query.FinalAnalysisComment

            };

            //ViewData["userId"] = userId;


                //query
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFinalAnalysisComment(FinalAnalysisComments model)
        {
            var status = false;
            var message = "";
            if (!ModelState.IsValid)
                return View(model);

            var data = await _context.ApplicantApplyForJob.FindAsync(model.Id);

            if (data == null)
                return NotFound();
            
               
            data.FinalAnalysisComment = model.FinalAnalysisComment;
            

             _context.ApplicantApplyForJob.Update(data);
            await _context.SaveChangesAsync();
            status = true;
            message = "Saved Successfully";

           // // return View();
            
           return Json(new { success = status, message = message });
            
        }

    }
}
