namespace CareJobApplicationv2.Models
{
    public class CandidatesEvaluators
    {
        public int Id { get; set; }
        public string EvaluatorId { get; set; }
        public int JobId { get; set; }
        public string EvaluationProccess { get; set; }

       public virtual Jobs Job { get; set; }
    }
}
