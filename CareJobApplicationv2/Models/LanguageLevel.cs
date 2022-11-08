using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class LanguageLevel
    {
        public int Id { get; set; }
        [Display(Name = "Skill Level")]
        public string LanguageLevelEn { get; set; }
        [Display(Name = "المستوى")]
        public string LanguageLevelAr { get; set; }

        [Display(Name = "IsActive")]
        public bool? IsActive { get; set; }

        [Display(Name = "css Class")]

        public string CssClass { get; set; }

        [InverseProperty("SpeakingLevels")]
        public virtual ICollection<ApplicantLanguageSkilles> SpeakingLevels { get; set; }
        [InverseProperty("ReadingLevels")]
        public virtual ICollection<ApplicantLanguageSkilles> ReadingLevels { get; set; }
        [InverseProperty("WritingLevels")]
        public virtual ICollection<ApplicantLanguageSkilles> WritingLevels { get; set; }
    }
}
