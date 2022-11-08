using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class ApplicantQ1Answers
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Person Name")]
        public string PersonNameEn { get; set; }

        [Display(Name = "الاسم")]
        public string personNameAr { get; set; }

        [Display(Name = "Relation Type")]
        public string RelationType { get; set; }

        public string UserId { get; set; }
    }
}
