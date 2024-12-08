namespace Web.Models
{
	public class Genre
	{
		public int Id { get; set; }
		public required string ApplicationUserId { get; set; }
		public int GenreTypeId { get; set; }
	}
}
