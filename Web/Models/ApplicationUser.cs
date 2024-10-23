using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
	[Index(nameof(Email), IsUnique = true)]
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(100)]
		public string? Name { get; set; }
		public string? AboutMe { get; set; }
		public string PicturePath { get; set; } = "~/images/profile-icon.png";
	}
}
