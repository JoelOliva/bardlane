using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Pages
{
    public class ProducersModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
        public List<ApplicationUser> Producers = [];
        public GenreType[] Genres = context.GenreTypes.ToArray();

		public async Task<IActionResult> OnGet()
        {
            var users = _userManager.Users.ToArray();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Producer"))
                    Producers.Add(user);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string selectedGenres)
        {
            string[] selectedGenresNames = [];
            // Handle the case when the user has unselected every genre
            if (selectedGenres != null)
				selectedGenresNames = selectedGenres.Split(',');

            List<Dictionary<string, string>> producers = [];
            var users = _userManager.Users.ToArray();
            foreach (var user in users)
            {
				Dictionary<string, string> producer = [];
				producer.Add("userName", user.UserName);
				producer.Add("picturePath", Url.Content(user.PicturePath));
                // Filter producers based on the selected genres
                if (await _userManager.IsInRoleAsync(user, "Producer"))
                {
                    if (selectedGenres != null)
                    {
                        var userGenres = _context.Genres.Where(g => g.ApplicationUserId == user.Id);
                        List<string> userGenresNames = [];
                        foreach (Genre userGenre in userGenres)
                        {
                            GenreType genreType = await _context.GenreTypes.Where(gt => gt.Id == userGenre.GenreTypeId).FirstAsync();
                            userGenresNames.Add(genreType.Name);
                        }

                        // Determine if a producer has all the selected genres
                        if (selectedGenresNames.All(sgn => userGenresNames.Contains(sgn)))
                        {
                            producers.Add(producer);
						}
                    }
                    else producers.Add(producer);
                }
            }

            return new JsonResult(producers);
        }
    }
}
