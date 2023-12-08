using examenporject_Frameworks_zenodesp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examenporject_Frameworks_zenodesp.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Display(Name = "Appointment")]
        public string Description { get; set; }
        [Display(Name = "Employee")]
        [ForeignKey("EmployeeUser")]
        public string EmployeeId { get; set; }
        public EmployeeUser Employee { get; set; }
        [Display(Name = "Regarding")]
        [ForeignKey("Complaint")]
        public int ComplaintId { get; set; }

        public Complaint? Complaint { get; set; }
        [Display(Name = "Created on")]
        [DataType(DataType.Date)]
        public DateTime created { get; set; } = DateTime.Now;
        [Display(Name = "Deleted on")]
        [DataType(DataType.Date)]
        public DateTime deleted { get; set; } = DateTime.MaxValue;

    }
}
