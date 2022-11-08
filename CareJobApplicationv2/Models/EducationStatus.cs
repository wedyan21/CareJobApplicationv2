using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class EducationStatus
    {
        public int Id { get; set; }

        public string StatusTitleEn { get; set; }

        public string StatusTitleAr { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true")]
        public bool? IsActive { get; set; }

        public string CssClass { get; set; }

        public virtual ICollection<ApplicantEducations> applicantEducations { get; set; }
    }
}
