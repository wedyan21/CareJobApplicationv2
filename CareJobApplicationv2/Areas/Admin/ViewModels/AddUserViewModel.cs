using CareJobApplicationv2.Models;
using CareJobApplicationv2.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Areas.Admin.ViewModels
{
    public class AddUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Is Care Team?")]
        public bool? IsCareTeam { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branchs Branchs { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
