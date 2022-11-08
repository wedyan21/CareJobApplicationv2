using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class Jobs
    {
        public int Id { get; set; }

        [Display(Name = "Job title")]

        public string JobTitle { get; set; }

        [Display(Name = "عنوان الوظيفة")]

        public string JobTitleAr { get; set; }

        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Display(Name = "الوصف")]
        public string JobDescriptionAr { get; set; }

        [Display(Name = "Publish Date")]

        public DateTime? PublishDate { get; set; }

        [Display(Name = "Deadline")]

        public DateTime? Deadline { get; set; }

        [Display(Name = "Category")]
        [ForeignKey("Categories")]

        public int? CategoriesId { get; set; }

        [Display(Name = "Branch")]
        [ForeignKey("Branchs")]
        public int? BranchsId { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]

        public DateTime? CreatedDate { get; set; }


        [Display(Name = "Attached File")]
        public string AttachedFile { get; set; }

        [Display(Name = "Published?")]
        public bool? Published { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("JobStatus")]
        public int StatusId { get; set; }

        [Display(Name = "SharePoint URL")]
        public string SharePointURL { get; set; }


        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }


        public string JobForm { get; set; }




        public virtual Branchs Branchs { get; set; }
        public virtual Categories Categories { get; set; }

        public virtual JobStatus JobStatus { get; set; }

        public virtual ICollection<ApplicantApplyForJob> ApplyForJobs { get; set; } 
        public virtual ICollection<CandidatesEvaluators> CandidatesEvaluators { get; set;}
        public virtual ICollection<InterviewSchedule> InterviewsSchedules { get; set;}
    }
}
