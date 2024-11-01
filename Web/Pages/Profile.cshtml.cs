using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Models;

namespace Web.Pages
{
    public class ProfileModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment env) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
        private readonly IWebHostEnvironment _env = env;
        public required string UserName;
        public required string Email;
        public string? AboutMe;
        public required string PicturePath;

        public async Task<IActionResult> OnGetAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            UserName = user.UserName;
            Email = user.Email;
            AboutMe = user.AboutMe;
            PicturePath = user.PicturePath;
            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file, string title)
        {
            // Add file to filesystem
            string[] filenameParts  = file.FileName.Split('.');
            string fileExtension = "." + filenameParts[filenameParts.Length - 1];
            string path = Path.Combine(_env.WebRootPath, "audio", Path.GetRandomFileName() + fileExtension);
            using (FileStream destination = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(destination);
            }

            // Add file to database
            var user = await _userManager.GetUserAsync(User);
            Track track = new Track { ApplicationUserId = user.Id, Title = title, Path = path };
            await _context.Tracks.AddAsync(track);
			await _context.SaveChangesAsync();

            return Page();
        }
    }
}
