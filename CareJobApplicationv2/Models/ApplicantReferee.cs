using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class ApplicantReferee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Referee First Name")]

        public string RefereeFirstNameEn { get; set; }
        [Display(Name = "الإسم الأول")]

        public string RefereeFirstNameAr { get; set; }

        [Display(Name = "Referee Last Name")]

        public string RefereeLastNameEn { get; set; }

        [Display(Name = "اللقب")]

        public string RefereeLastNameAr { get; set; }

        [Display(Name = "Organization Name")]

        public string RefereeOrganizationEn { get; set; }

        [Display(Name = "إسم المؤسسة")]

        public string RefereeOrganizationAr { get; set; }

        [Display(Name = "Referee Job Title")]

        public string RefereeJobTitleEn { get; set; }
        [Display(Name = "المنصب الاداري")]

        public string RefereeJobTitleAr { get; set; }
        [Display(Name = "Referee Phone No.")]

        [DataType(DataType.PhoneNumber)]
        public string RefereePrimaryPhone { get; set; }

        [Display(Name = "Referee E-mail")]

        [DataType(DataType.EmailAddress)]
        public string RefereeEmail { get; set; }

        public string UserId { get; set; }
    }
}
