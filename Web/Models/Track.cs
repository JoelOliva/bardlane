namespace Web.Models
{
	public class Track
	{
		public int Id { get; set; }
		public int ApplicationUserId { get; set; }
		public required string Title { get; set; }
		public required float Duration { get; set; }
		public required string Path { get; set; }
	}
}
