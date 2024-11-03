using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Data
{
	public class DbSeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
	{
		private readonly RoleManager<IdentityRole> _roleManager = roleManager;
		private readonly UserManager<ApplicationUser> _userManager = userManager;

		public async Task Seed()
		{
			if (!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new IdentityRole("Admin"));
				await _roleManager.CreateAsync(new IdentityRole("Producer"));

				ApplicationUser user = new() { UserName = "admin", Email = "admin@gmail.com" };
				await _userManager.CreateAsync(user, "Password_84");
				await _userManager.AddToRoleAsync(user, "Admin");
			}
		}
	}
}
