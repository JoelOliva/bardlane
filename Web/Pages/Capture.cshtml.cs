using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaypalServerSdk.Standard.Models;
using Web.Utilities;

namespace Web.Pages
{
    public class CaptureModel(PayPal payPal) : PageModel
    {
        private readonly PayPal _payPal = payPal;

        [BindProperty(SupportsGet = true)]
		public required string OrderId { get; set; }

        public async Task<IActionResult> OnPost()
        {
            Order? order = await _payPal.CaptureOrder(OrderId);

            if (order != null)
                return new JsonResult(order);

            return Page();
        }
    }
}
