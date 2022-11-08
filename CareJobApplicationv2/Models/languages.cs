using System.ComponentModel.DataAnnotations;

namespace CareJobApplicationv2.Models
{
    public class languages
    {
        public int Id { get; set; }

        [Display(Name = "Language Code")]
        public string language_code { get; set; }
        [Display(Name = "Language")]
        public string language_name { get; set; }
    }
}
