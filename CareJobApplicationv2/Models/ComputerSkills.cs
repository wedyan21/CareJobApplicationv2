using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ComputerSkills
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Skill")]
        public string SkillTitleEnglish { get; set; }
        [Display(Name = "Skill")]
        public string SkillTitleArabic { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }  
    }
}
