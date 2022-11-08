using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ApplicantComputerSkills
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Skill")]
        
        public int SkillId { get; set; }
        [Display(Name = "Skill level")]
        public int SkillLevelId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("SkillId")]
        public virtual  ComputerSkills ComputerSkills { get; set; }
        [ForeignKey("SkillLevelId")]
        public virtual ComputerSkillLevels ComputerSkillLevels { get; set; } 
    }
}
