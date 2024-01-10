using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace examenporject_Frameworks_zenodesp.Areas.Identity.Data
{
    public class EmployeeUser : IdentityUser
    {

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string? Department { get; set; }
    }
}
