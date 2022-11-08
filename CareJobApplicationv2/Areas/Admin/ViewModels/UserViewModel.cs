using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CareJobApplicationv2.Models;
using Microsoft.AspNetCore.Identity;

namespace CareJobApplicationv2.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
       
       // [ForeignKey("Branchs")]
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public bool? IsCareTeam { get; set; }

        public IEnumerable<string> Roles { get; set; }

        //public IEnumerable<Branchs> Branchs { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branchs Branchs { get; set; }
    }
}
