using System.ComponentModel.DataAnnotations;

namespace examenporject_Frameworks_zenodesp.Models
{
	public class UserIndexModel
	{
		[Display(Name = "Username")]
		public string UserName { get; set; }
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
		[Display(Name = "Blocked?")]
		public Boolean Blocked { get; set; }
		[Display(Name = "Roles")]
		public List<string> Roles { get; set; }
	}

	public class RoleViewModel
	{
		[Display(Name = "Username")]
		public string UserName { get; set; }
		[Display(Name = "Roles")]
		public List<string> Roles { get; set; }

	}
}
