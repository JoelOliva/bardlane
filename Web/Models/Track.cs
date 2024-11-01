namespace Web.Models
{
	public class Track
	{
		public int Id { get; set; }
		public required string ApplicationUserId { get; set; }
		public required string Title { get; set; }
		public required string Path { get; set; }
	}
}
