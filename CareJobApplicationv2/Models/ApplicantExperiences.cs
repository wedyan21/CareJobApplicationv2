using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class ApplicantExperiences
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employer name is requierd")]
        public string EmployerName { get; set; }
        [Required(ErrorMessage = "Position is requierd")]
        public string Position { get; set; }
        public string Tasks { get; set; }
        [Required(ErrorMessage ="Start Date is requierd")]
        
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Reasonforleave { get; set; }
        public string UserId { get; set; }


    }
}
