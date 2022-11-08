using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ApplicantApplyForJob
    {
        [Key]
        public int Id { get; set; }
        public DateTime ApplyDate { get; set; }
        [ForeignKey("Job")]
        public int JobId { get; set; }

        public string UserId { get; set; }

        public string JobFormData { get; set; }

        public bool? IsLongList { get; set; }

        public bool? IsInterViewList { get; set; }

        public bool? IsWinner { get; set; }

        public string FinalAnalysisComment { get; set; }

        public virtual Jobs Job { get; set; }

        
        
    }
}
