using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WebApplication3.ViewModels;
using System.Threading.Tasks;

namespace WebApplication3.Pages
{
	public class LogoutModel : PageModel
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LogoutModel(SignInManager<ApplicationUser> signInManager,IHttpContextAccessor context)
		{
			this.signInManager = signInManager;
			_httpContextAccessor = context;
		}
		public void OnGet() { }
		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await signInManager.SignOutAsync();
			_httpContextAccessor.HttpContext.Session.Clear();
			return RedirectToPage("Login");
		}
		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}
	}
}
