using CareJobApplicationv2.Areas.Admin.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class Branchs
    {
        public int Id { get; set; }

        [Display(Name = "Branch")]

        public string BranchTitleEn { get; set; }


        [Display(Name = "الفرع")]

        public string BranchTitleAr { get; set; }


        [Display(Name = "Order")]
        public int? GovOrder { get; set; }

        [Display(Name = "Is it Area Branch")]
        public bool? IsArea { get; set; }

        [Display(Name = "Parent Branch")]
        public int? ParentId { get; set; }

        [Display(Name = "Enable")]
        public bool? GovEnable { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }

        //public virtual ICollection<UserViewModel> Users { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }
    }
}
