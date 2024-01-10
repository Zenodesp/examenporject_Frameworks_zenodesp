using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using examenporject_Frameworks_zenodesp.Models;
using examenporject_Frameworks_zenodesp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace examenporject_Frameworks_zenodesp.Data
{
    public class ApplicationDbContext : IdentityDbContext<EmployeeUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<examenporject_Frameworks_zenodesp.Models.Complaint> Complaints { get; set; } = default!;
        public DbSet<examenporject_Frameworks_zenodesp.Models.Appointment> Appointment { get; set; } = default!;

        public static async Task DataInitialiser(ApplicationDbContext context, UserManager<EmployeeUser> userManager)
        {

            if (!context.Languages.Any())
            {
                context.AddRange(
                    new Language { Id = "-", Name = "-", IsSystemLanguage = false, isAvailable = DateTime.MinValue },
                    new Language { Id = "en", Name = "English", IsSystemLanguage = true },

                    new Language { Id = "nl", Name = "Nederlands", IsSystemLanguage = true },

                    new Language { Id = "fr", Name = "Français", IsSystemLanguage = true },

                    new Language { Id = "de", Name = "Deutsch", IsSystemLanguage = true }
                    );
                context.SaveChanges();
            }

            Language.GetLanguages(context);

            if (!context.Users.Any())
            {
                EmployeeUser dummyUser = new EmployeeUser
                {
                    Id = "Dummy",
                    Email = "Dummy@dumdum.com",
                    UserName = "Dummy",
                    FirstName = "Dummy",
                    LastName = "Dummy",
                    Department = "Dummy",
                    PasswordHash = "Dummy",
                    LockoutEnabled = true,
                    LockoutEnd = DateTime.MaxValue
                };
                EmployeeUser AdminUser = new EmployeeUser
                {
                    Id = "Admin",
                    Email = "Admin@Admin.com",
                    UserName = "Admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Department = "Admin",
                    PasswordHash = "Admin",
                    LockoutEnabled = true,
                    LockoutEnd = DateTime.MaxValue
                };

                var result = await userManager.CreateAsync(AdminUser, "Abc!12345");

                context.Users.Add(dummyUser);
                context.SaveChanges();


            }


            EmployeeUser dummy = context.Users.First(g => g.UserName == "Dummy");
            EmployeeUser admin = context.Users.First(g => g.UserName == "Admin");
            ComplaintType dummyType = context.ComplaintType.First(g => g.TypeName == "dummy");

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole { Name = "SystemAdministrator", Id = "SystemAdministrator", NormalizedName = "SystemAdministrator" },
                    new IdentityRole { Name = "User", Id = "User", NormalizedName = "USER" });

                context.UserRoles.Add(new IdentityUserRole<string> { RoleId = "SystemAdministrator", UserId = admin.Id });
                context.UserRoles.Add(new IdentityUserRole<string> { RoleId = "User", UserId = dummy.Id });
                context.SaveChanges();
            }

            if (!context.Complaints.Any())
            {
                context.Complaints.Add(new Complaint { Description = "dummy", EmployeeId = dummy.Id, complaintLogger = dummy, ComplaintTypeId = dummyType.Id, created = DateTime.Now, deleted = DateTime.Now });
                context.SaveChanges();
            }
             Complaint dummyComplaint = context.Complaints.First(g => g.Description == "dummy");
            if (!context.Appointment.Any())
            {
                context.Appointment.Add(new Appointment { Description = "dummy", Employee = dummy, Complaint = dummyComplaint, created = DateTime.Now, deleted = DateTime.Now });
                context.SaveChanges();
            }
            if (!context.ComplaintType.Any())
            {
                context.ComplaintType.Add(new ComplaintType { Id = "dummy", TypeName = "dummy" });
                context.SaveChanges();
            }


        }

        public DbSet<examenporject_Frameworks_zenodesp.Models.ComplaintType> ComplaintType { get; set; } = default!;

        public DbSet<examenporject_Frameworks_zenodesp.Models.Language> Languages { get; set; } = default!;


    }
}