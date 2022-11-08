using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class ComputerSkillLevels
    {
        [Key]
        public int Id { get; set; }
       
        [Display(Name ="Level")]
        public string LevelTitleEnglish { get; set; }
        [Display(Name = "المستوى")]
        public string LevelTitleArabic { get; set; }
        [Display(Name = "Score")]
        public int LevelScore { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

       

    }
}
