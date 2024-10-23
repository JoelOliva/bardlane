using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;

namespace Web.Pages
{
    public class ProfileModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        public required string UserName;
        public required string Email;
        public string? AboutMe;
        public required string PicturePath;

        public async Task<IActionResult> OnGetAsync(string username)
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.FindByNameAsync(username);
            if (userName == null)
            {
                return NotFound(); // Handle case when user is not found
            }

            UserName = userName.UserName;
            Email = userName.Email;
            AboutMe = userName.AboutMe;
            PicturePath = user.PicturePath;
            return Page();
        }
    }
}
