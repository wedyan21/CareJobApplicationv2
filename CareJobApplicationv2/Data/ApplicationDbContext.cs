using CareJobApplicationv2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CareJobApplicationv2.ViewModels;

namespace CareJobApplicationv2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CareJobApplicationv2.Models.Branchs> Branchs { get; set; }
        public DbSet<CareJobApplicationv2.Models.Categories> Categories { get; set; }
        public DbSet<CareJobApplicationv2.Models.Jobs> Jobs { get; set; }

        public DbSet<CareJobApplicationv2.Models.JobStatus> JobStatus { get; set; }

        public DbSet<CareJobApplicationv2.Models.ApplicantApplyForJob> ApplicantApplyForJob { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantBrofile> ApplicantBrofile { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantExperiences> ApplicantExperiences { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantEducations> ApplicantEducations { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantDocuments> ApplicantDocuments { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantLanguageSkilles> ApplicantLanguageSkilles { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantReferee> ApplicantReferee { get; set; }
        
        public DbSet<CareJobApplicationv2.Models.EducationMajor> EducationMajor { get; set; }

        public DbSet<CareJobApplicationv2.Models.EducationDegree> EducationDegree { get; set; }

        public DbSet<CareJobApplicationv2.Models.Countries> Countries { get; set; }

        public DbSet<CareJobApplicationv2.Models.LanguageLevel> LanguageLevel { get; set; }
        public DbSet<CareJobApplicationv2.Models.languages> languages { get; set; }
        public DbSet<CareJobApplicationv2.Models.DocumentTypes> DocumentTypes { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantComputerSkills> ApplicantComputerSkills { get; set; }
        public DbSet<CareJobApplicationv2.Models.CandidatesEvaluators> CandidatesEvaluators { get; set; }
        public DbSet<CareJobApplicationv2.Models.InterviewCandidates> InterviewCandidates { get; set; }

        public DbSet<CareJobApplicationv2.Models.ExperiencePeriod> ExperiencePeriod { get; set; }

        public DbSet<CareJobApplicationv2.Models.InterviewSchedule> InterviewSchedule { get; set; }

        public DbSet<CareJobApplicationv2.Models.InterviewPanel> InterviewPanel { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantQ1Answers> ApplicantQ1Answers { get; set; }
        public DbSet<CareJobApplicationv2.Models.ApplicantQ2Answers> ApplicantQ2Answers { get; set; }
        public DbSet<CareJobApplicationv2.Models.EducationStatus> EducationStatus { get; set; }


    }
}