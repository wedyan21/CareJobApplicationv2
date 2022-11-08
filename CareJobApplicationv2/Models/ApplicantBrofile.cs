using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ApplicantBrofile
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]

        public string FullNameEn { get; set; }

        [Display(Name = "لإسم الكامل")]
        [Required(ErrorMessage = "يرجى إدخال الإسم الكامل ")]

        public string FullNameAr { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender Type is required")]
        // public int? GenderId { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender GenderId { get; set; }

        [Display(Name = "Nationality")]

        public string Nationality { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required")]
        public string MobileNumber { get; set; }


        [Display(Name = "Upload personal Photo")]
        [DataType(DataType.Upload)]

        public string ApplicantPhoto { get; set; }


        [Display(Name = "FieldLable_CurrentAddress")]

        public string Address { get; set; }



        [Display(Name = "Current Address Country")]

        public string Country { get; set; }

        [Display(Name = "Current Address City")]

        public string City { get; set; }

        public string UserId { get; set; }
        
        [DataType("Email")]
        public string Email { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
