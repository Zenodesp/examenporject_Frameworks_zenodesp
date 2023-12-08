using examenporject_Frameworks_zenodesp.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examenporject_Frameworks_zenodesp.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        [Display(Name = "Complaint")]
        public string Description { get; set; }
        [Display(Name = "Posted by")]
        [ForeignKey("EmployeeUser")]
        public string EmployeeId { get; set; }
        public EmployeeUser? complaintLogger { get; set; }

        [Display(Name = "Posted on")]
        [DataType(DataType.Date)]
        public DateTime created { get; set; } = DateTime.Now;
        [Display(Name = "Deleted on")]
        [DataType(DataType.Date)]
        public DateTime deleted { get; set; } = DateTime.MaxValue;
    }

    public class ComplaintIndexViewModel
    {
        public List<Complaint> Complaints { get; set; }
        public string SelectMode { get; set; }
        public SelectList Modes { get; set; }
    }

    public class modeItem
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}
