using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaypalServerSdk.Standard.Models;
using Web.Data;
using Web.Utilities;

namespace Web.Pages
{
    public class CheckoutModel(PayPal payPal, ApplicationDbContext context) : PageModel
    {
        private readonly PayPal _payPal = payPal;
        private readonly ApplicationDbContext _context = context;

        [BindProperty(SupportsGet = true)]
		public required string PackageId { get; set; }

        public async Task<IActionResult> OnPost()
        {
            int amount = _context.Packages.Where(p => p.Id == Int32.Parse(PackageId)).First().Price;
            Order? order = await _payPal.CreateOrder(amount.ToString());

            if (order != null)
                return new JsonResult(order);

            return Page();
        }
    }
}
