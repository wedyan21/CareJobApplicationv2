namespace CareJobApplicationv2.Models
{
    public class InterviewPanel
    {
        public int Id { get; set; }

        public int InterviewScheduleId { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public InterviewSchedule InterviewSchedule { get; set; }
    }
}
