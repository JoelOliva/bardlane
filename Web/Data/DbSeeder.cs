using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Data
{
	public class DbSeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
	{
		private readonly RoleManager<IdentityRole> _roleManager = roleManager;
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly ApplicationDbContext _context = context;

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
			if (!_context.PackageTypes.Any())
			{
				PackageType packageType1 = new() { Name = "Basic" };
				PackageType packageType2 = new() { Name = "Standard" };
				PackageType packageType3 = new() { Name = "Premium" };
				await _context.PackageTypes.AddAsync(packageType1);
				await _context.PackageTypes.AddAsync(packageType2);
				await _context.PackageTypes.AddAsync(packageType3);
				await _context.SaveChangesAsync();
			}
			if (!_context.GenreTypes.Any())
			{
				HashSet<GenreType> genres = [];
				genres.Add(new() { Name = "Rock" });
				genres.Add(new() { Name = "Jazz" });
				genres.Add(new() { Name = "Classical" });
				genres.Add(new() { Name = "Techno" });
				genres.Add(new() { Name = "Country" });
				genres.Add(new() { Name = "Folk" });
				genres.Add(new() { Name = "Funk" });
				genres.Add(new() { Name = "Ambient" });
				genres.Add(new() { Name = "Latin" });

				foreach (GenreType genre in genres)
				{
					await _context.GenreTypes.AddAsync(genre);
				}
				await _context.SaveChangesAsync();
			}
		}
	}
}
