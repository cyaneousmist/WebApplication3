using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WebApplication3.ViewModels;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace WebApplication3.Pages
{
	public class LoginModel : PageModel
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<ApplicationUser> userManager;
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<ApplicationUser> signInManager;
		public LoginModel(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.httpContextAccessor = httpContextAccessor;
		}
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
            if (ModelState.IsValid)
            {
				var user = await userManager.FindByEmailAsync(LModel.Email);
				var firstName = httpContextAccessor.HttpContext.Session.GetString("FirstName");
				var lastName = httpContextAccessor.HttpContext.Session.GetString("LastName");
				var email = httpContextAccessor.HttpContext.Session.GetString("Email");
				var password = httpContextAccessor.HttpContext.Session.GetString("Password");

				/*var user = new ApplicationUser
				{
					UserName = firstName,
					Email = email,
					PasswordHash = password
				};*/

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, false, false);

                    if (result.Succeeded)
                    {
						httpContextAccessor.HttpContext.Session.SetString("FirstName", user.FirstName);
						httpContextAccessor.HttpContext.Session.SetString("LastName", user.LastName);
						httpContextAccessor.HttpContext.Session.SetString("Email", user.Email);
						httpContextAccessor.HttpContext.Session.SetString("CreditCard", user.CreditCard);
						httpContextAccessor.HttpContext.Session.SetString("BillingAddress", user.BillingAddress);
						httpContextAccessor.HttpContext.Session.SetString("ShippingAddress", user.ShippingAddress);
					
						httpContextAccessor.HttpContext.Session.SetString("Photo", user.Photo);
						return RedirectToPage("/Index");
                    }
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }
            return Page();
		}
	}
}

