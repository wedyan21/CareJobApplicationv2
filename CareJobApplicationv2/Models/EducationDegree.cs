using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class EducationDegree
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Degree Title")]
        public string EducationDegreeTitle { get; set; }

        [Display(Name = "Arabic Degree Title")]
        public string EducationDegreeTitle_Ar { get; set; }

        [Display(Name = "Status")]
        public bool Active { get; set; }

        public int Score { get; set; }

        public virtual ICollection<ApplicantEducations> Educations { get; set; } 
    }
}
