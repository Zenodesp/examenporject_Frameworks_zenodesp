using Microsoft.AspNetCore.Identity;

namespace examenporject_Frameworks_zenodesp.Areas.Identity.Data
{
    public class EmployeeUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Department { get; set; }
    }
}
