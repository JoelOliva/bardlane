using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
	[Index(nameof(Name), IsUnique = true)]
	public class GenreType
	{
		public int Id { get; set; }
		public required string Name { get; set; }
	}
}
