using Web.Models;

namespace Web.Data
{
	public class DbSeeder
	{
		private readonly ApplicationDbContext _context;

		public DbSeeder(ApplicationDbContext context) {
			_context = context;
		}

		public void Seed()
		{
			//if (!_context.ApplicationUsers.Any())
			//{
			//	_context.ApplicationUsers.AddRange(
			//		new ApplicationUser { UserName = "rsmith99", Name = "Robert Smith", PicturePath = "https://www.adobe.com/creativecloud/photography/discover/media_1211f0f1379c0ec7b3758e0970180a380cff91073.png?width=750&format=png&optimize=medium" }
			//	);
			//	_context.SaveChanges();
			//}
		}
	}
}
