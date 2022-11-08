using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class ApplicantQ2Answers
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Have you worked or trained in care?")]
        public bool WorkingWithCare { get; set; }

        [Display(Name = "Type of contract")]
        public int ContractType { get; set; }

        [Display(Name = "work/training Start Date:")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]

        public DateTime WorkingFrom { get; set; }

        [Display(Name = "work/training Finished Date:")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime WorkingTo { get; set; }

        public string UserId { get; set; }
    }
}
