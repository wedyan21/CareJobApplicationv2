using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ApplicationUser:IdentityUser
    {
       // [MaxLength(128)]
       // public string FirstName { get; set; }

       // [MaxLength(128)]
       // public string LastName { get; set; }

       // public int GroupId { get; set; }
        public int BranchId { get; set; }

        public bool? IsCareTeam { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branchs Branchs { get; set; }

    }
}
