using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
	[Index(nameof(Name), IsUnique = true)]
	public class PackageType
	{
		public int Id { get; set; }
		public required string Name { get; set; }
	}
}
