using examenporject_Frameworks_zenodesp.Areas.Identity.Data;
using examenporject_Frameworks_zenodesp.Data;
using examenporject_Frameworks_zenodesp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace examenporject_Frameworks_zenodesp.Controllers
{
	[Authorize(Roles = "SystemAdministrator")]
	public class UsersController : Controller
	{
		ApplicationDbContext _context;

		public UsersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
		}

		public IActionResult Index(string userName, string firstName, string lastName, string email)
		{
			List<UserIndexModel> indexList = new List<UserIndexModel>();

			List<EmployeeUser> users = _context.Users.Where(u => u.UserName != "Dummy"
																		&& (u.UserName.Contains(userName) || string.IsNullOrEmpty(userName))
																		&& (u.FirstName.Contains(firstName) || string.IsNullOrEmpty(firstName))
																		 && (u.LastName.Contains(lastName) || string.IsNullOrEmpty(lastName))
																		  && (u.Email.Contains(email) || string.IsNullOrEmpty(email))).ToList();
			foreach(EmployeeUser user in users)
			{
				indexList.Add(new UserIndexModel
				{
					Blocked = !user.LockoutEnabled,
					Email = user.Email,
					FirstName = user.FirstName,
					LastName = user.LastName,
					UserName = user.UserName,
					Roles = (from userRole in _context.UserRoles
							 where userRole.UserId == user.Id
							 orderby userRole.RoleId
							 select userRole.RoleId).ToList()
				});


			}
			ViewData["userName"] = userName;
			ViewData["firstName"] = firstName;
			ViewData["lastName"] = lastName;
			ViewData["e-mail"] = email;

			return View(indexList);
		}

		public IActionResult UnBlock(string userName)
        {
            EmployeeUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            user.LockoutEnabled = true;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Block(string userName)
        {
            EmployeeUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            user.LockoutEnabled = false;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Roles(string userName)
        {
            EmployeeUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            RoleViewModel model = new RoleViewModel
            {
                UserName = userName,
                Roles = (from userRole in _context.UserRoles
                         where userRole.UserId == user.Id
                         orderby userRole.RoleId
                         select userRole.RoleId).ToList()
            };
            ViewBag.RoleIds = new MultiSelectList(_context.Roles.OrderBy(r => r.Name), "Id", "Name", model.Roles);
            return View(model);
        }
    }
}
