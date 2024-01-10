using System.ComponentModel.DataAnnotations;

namespace examenporject_Frameworks_zenodesp.Models
{
    public class ComplaintType
    {
        public string Id { get; set; }
        [Display(Name = "Complaint Type")]
        public string TypeName { get; set; }

        public DateTime created { get; set; } = DateTime.Now;
        public DateTime deleted { get; set; } = DateTime.MaxValue;
    }
}
