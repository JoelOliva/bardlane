namespace Web.Models
{
	public class Package
	{
		public int Id { get; set; }
		public required string ApplicationUserId { get; set; }
		public int PackageTypeId { get; set; }
		public required int Price { get; set; }
		public required string Description { get; set; }
		public required int DeliveryTime { get; set; }
	}
}
