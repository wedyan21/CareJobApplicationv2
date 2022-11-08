using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class InterviewCandidates
    {
        public int Id { get; set; }
        
        public int JobId { get; set; }
        public int InterviewScheduleId { get; set; }

        public string CandidateId { get; set; }

        [DisplayName("Interview Date")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? InterViewDate { get; set; }
        
        [DisplayName("Interview Time")]
        [BindProperty, DataType(DataType.Time)]
        public DateTime? InterviewTime { get; set; }

        [DisplayName("Comments")]

        public string Comments { get; set; }

        public int ApplicantBrofileId { get; set; }

        public InterviewSchedule InterviewSchedule { get; set; }

        public ApplicantBrofile ApplicantBrofile { get; set; }

       


    }
}
