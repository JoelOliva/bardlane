using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Pages
{
	public class IndexModel : PageModel
	{
		private static readonly int MAX_SONGS = 10;
		private readonly ILogger<IndexModel> _logger;
		private readonly ApplicationDbContext _context;
		public Track[] Tracks = [];
		public List<string> TrackTitles = [];
		public List<string> TrackAuthors = [];

		public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> OnGet()
		{
			Tracks = await _context.Tracks
					.OrderBy(e => Guid.NewGuid())
					.Take(10)
					.ToArrayAsync();

            for (int i = 0; i < Tracks.Length; i++)
            {
                TrackTitles.Add(Tracks[i].Title);
				var user = await _context.ApplicationUsers.Where(u => u.Id == Tracks[i].ApplicationUserId).FirstAsync();
				TrackAuthors.Add(user.UserName);
            }

			return Page();
		}
	}
}
