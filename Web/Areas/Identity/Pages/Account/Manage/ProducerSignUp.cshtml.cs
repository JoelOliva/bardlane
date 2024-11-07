using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    [BindProperties]
    public class ProducerSignUpModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;


        public required string AboutMe { get; set; }
        public int BasicPrice { get; set; }
        public int StandardPrice { get; set; }
        public int PremiumPrice { get; set; }
        public int BasicDeliveryTime { get; set; }
        public int StandardDeliveryTime { get; set; }
        public int PremiumDeliveryTime { get; set; }
        public required string BasicDescription { get; set; }
        public required string StandardDescription { get; set; }
        public required string PremiumDescription { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            user.AboutMe = AboutMe;
            await _userManager.AddToRoleAsync(user, "Producer");

            var basicPackageType = await _context.PackageTypes.Where(t => t.Name == "Basic").FirstAsync();
            var standardPackageType = await _context.PackageTypes.Where(t => t.Name == "Standard").FirstAsync();
            var premiumPackageType = await _context.PackageTypes.Where(t => t.Name == "Premium").FirstAsync();
            Package basic = new() {
                ApplicationUserId = user.Id,
                PackageTypeId = basicPackageType.Id,
                Price = BasicPrice,
                DeliveryTime = BasicDeliveryTime,
                Description = BasicDescription
            };
            Package standard = new() {
                ApplicationUserId = user.Id,
                PackageTypeId = standardPackageType.Id,
                Price = StandardPrice,
                DeliveryTime = StandardDeliveryTime,
                Description = StandardDescription
            };
            Package premium = new() {
                ApplicationUserId = user.Id,
                PackageTypeId = premiumPackageType.Id,
                Price = PremiumPrice,
                DeliveryTime = PremiumDeliveryTime,
                Description = PremiumDescription
            };

            await _context.Packages.AddAsync(basic);
            await _context.Packages.AddAsync(standard);
            await _context.Packages.AddAsync(premium);
            await _context.SaveChangesAsync();

            return Redirect("../ProducerCreated");
        }
    }
}
