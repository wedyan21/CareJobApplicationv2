using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class DocumentTypes
    {
        public int Id { get; set; }
        [Display(Name = "Document Type")]
        public string DocumentTypeEn { get; set; }
        [Display(Name = "نوع المستند")]
        public string DocumentTypeAr { get; set; }

        public bool? IsActive { get; set; }

        
        public virtual ICollection<ApplicantDocuments> ApplicantDocuments { get; set; }
    }
}
