using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;

namespace Web.Pages
{
    public class ProducersModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        public List<ApplicationUser> Producers = [];

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
    }
}
