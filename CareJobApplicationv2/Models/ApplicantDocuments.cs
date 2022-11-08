using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareJobApplicationv2.Models
{
    public class ApplicantDocuments
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Document Type")]
        [ForeignKey("DocumentTypes")]
        public int DocumentType { get; set; }


        [DataType(DataType.Upload)]
        public string Document { get; set; }

        
        public string DocumentDescription { get; set; }


        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        public virtual DocumentTypes DocumentTypes { get; set; }
    }
}
