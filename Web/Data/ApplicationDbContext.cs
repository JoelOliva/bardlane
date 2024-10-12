using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Package> Packages { get; set; }
		public DbSet<PackageType> PackageTypes { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Link> Links { get; set; }
	}
}
