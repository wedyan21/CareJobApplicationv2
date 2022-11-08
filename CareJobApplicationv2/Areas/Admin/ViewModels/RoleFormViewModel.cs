using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Areas.Admin.ViewModels
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage ="Role name is Required ") ]
        public string Name { get; set; }
    }
}
