using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Models;

namespace Web.Pages
{
    public class ProducersModel(ApplicationDbContext context) : PageModel
    {
        private readonly ApplicationDbContext _context = context;
        public required ApplicationUser[] Users;

		public void OnGet()
        {
            Users = _context.ApplicationUsers.ToArray();
        }
    }
}
