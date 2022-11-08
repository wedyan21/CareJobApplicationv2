using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ApplicantEducations
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Education Degree")]
        [Required(ErrorMessage ="Please Select Degree")]
        [ForeignKey("EducationDegree")]
        public int EducationDegreeId { get; set; }

        [Display(Name = "Education Major")]
        [Required(ErrorMessage = "Please Select Major")]
        [ForeignKey("EducationMajor")]
        public int EducationMajorId { get; set; }

        [Display(Name = "Education Country")]
        [Required(ErrorMessage = "Please Select Country of Education")]
        //[ForeignKey("Countries")]
        public string CountryEducation { get; set; }

        [Display(Name = "Institution")]
        [Required(ErrorMessage = "Please write institution ")]
        public string InstitutionEn { get; set; }

        [Display(Name = "إسم المؤسسة التعليمية")]

        public string InstitutionAr { get; set; }

        [Display(Name = "Education Status")]
        [ForeignKey("EducationStatus")]
        public int Status { get; set; }

        [Display(Name = "Year of Starting")]
        [DataType(DataType.Date)]
        public DateTime YearOfStarting { get; set; }

        [Display(Name = "Year of Finished")]
        [DataType(DataType.Date)]
        public DateTime YearOfFinished { get; set; }

        public string UserId { get; set; }

        
        public virtual EducationMajor EducationMajor { get; set; }

        
        public virtual EducationDegree EducationDegree { get; set; }

        public virtual EducationStatus EducationStatus { get; set; }

        //public virtual Countries Countries { get; set; }
    }
}
