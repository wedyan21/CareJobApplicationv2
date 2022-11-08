using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class JobStatus
    {
        public int Id { get; set; }

        [Display(Name = "Job Status")]
        public string JobStatuse { get; set; }
        [Display(Name = "Value for Projress bar")]
        public int? ProjressValue { get; set; }

        [Display(Name = "Status Color")]
        public string StatusColor { get; set; }
                
        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
