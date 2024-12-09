#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Models;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
			IWebHostEnvironment env
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _env = env;
        }

		public string PicturePath { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
			public string UserName { get; set; }
			public string Name { get; set; }
            public string AboutMe { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var UserName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            PicturePath = user.PicturePath;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
				UserName = UserName,
                Name = user.Name,
                AboutMe = user.AboutMe
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var UserName = await _userManager.GetUserNameAsync(user);
            if (Input.UserName != UserName)
            {
                // TODO: Check if user name is taken
                //if (await _userManager.FindByNameAsync(Input.UserName) != null)
                //{

                //}

                var result = await _userManager.SetUserNameAsync(user, Input.UserName);
                if (!result.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!result.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }


            if (file != null)
            {
                // Add file to filesystem
                string[] filenameParts = file.FileName.Split('.');
                string fileExtension = "." + filenameParts[filenameParts.Length - 1];
                string fileName = Path.GetRandomFileName() + fileExtension;
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                using (FileStream destination = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(destination);
                }

                // Add file to database
                string relativePath = Path.Combine("~/images", fileName);
                user.PicturePath = relativePath;
            }

            user.Name = Input.Name;
            user.AboutMe = Input.AboutMe;
            await _context.SaveChangesAsync();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
