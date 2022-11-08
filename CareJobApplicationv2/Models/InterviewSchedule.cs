using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class InterviewSchedule
    {
        public int Id { get; set; }
        [DisplayName("Jobs Title")]
        public int JobId { get; set; }

        [DisplayName("Interview Location")]
        public string Location { get; set; }

        [DisplayName("Interview Date")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? InterviewDate { get; set; }
        
        [DisplayName("Interview Day")]
        public string InterviewDay { get; set; }

        
        [DisplayName("Interview Time")]
        [BindProperty, DataType(DataType.Time)]
        public DateTime? InterviewTime { get; set;}

        public virtual ICollection<InterviewPanel> InterviewPanels { get; set; }

        public virtual ICollection<InterviewCandidates> interviewCandidates { get; set; }

        public Jobs Job { get; set; }



        


    }
}
