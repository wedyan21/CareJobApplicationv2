using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class Categories
    {
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryNameEn { get; set; }

        [Display(Name = "إسم التصنيف")]
        public string CategoryNameAr { get; set; }

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        [Display(Name = "الوصف")]
        public string CategoryDescriptionAr { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
