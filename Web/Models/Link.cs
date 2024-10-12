namespace Web.Models
{
	public class Link
	{
		public int Id { get; set; }
		public int ApplicationUserId { get; set; }
		public required string Url { get; set; }
	}
}
