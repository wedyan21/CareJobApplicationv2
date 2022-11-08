using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class EducationMajor
    {
        public int Id { get; set; }

        [Display(Name = "Education Major Title")]
        public string EducationMajorTitle { get; set; }

        [Display(Name = "Education Major Arabic Title")]
        public string EducationMajorTitleAr { get; set; }

        [Display(Name = "Status")]
        public bool Active { get; set; }

        public virtual ICollection<ApplicantEducations> Educations { get; set; }
        
    }
}
