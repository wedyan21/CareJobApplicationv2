using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ApplicantLanguageSkilles
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Language")]
        public string LanguageName { get; set; }

        [Display(Name = "Speaking Proficiency")]
        
        public int SpeakingLevel { get; set; }

        [Display(Name = "Reading Proficiency")]
        
        public int? ReadingLevel { get; set; }

        [Display(Name = "Writing Proficiency")]
        
        public int WritingLevel { get; set; }

        public string UserId { get; set; }

        [ForeignKey("SpeakingLevel")]
        public virtual LanguageLevel SpeakingLevels { get; set; }
        [ForeignKey("ReadingLevel")]
        public virtual LanguageLevel ReadingLevels { get; set; }
        [ForeignKey("WritingLevel")]
        public virtual LanguageLevel WritingLevels { get; set; }
    }

    
}
