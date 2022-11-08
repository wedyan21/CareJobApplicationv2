using CareJobApplicationv2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Areas.Admin.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Is Care Team?")]
        public bool IsCareTeam { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branchs Branchs { get; set; }

       
    }
}
