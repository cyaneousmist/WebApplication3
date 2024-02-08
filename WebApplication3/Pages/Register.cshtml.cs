using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WebApplication3.ViewModels;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Security.Claims;

namespace WebApplication3.Pages
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CreditCard { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BillingAddress { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
    }
    public class RegisterModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }



        public void OnGet()
        {
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //check for duplicate email
                var emailtest = await userManager.FindByEmailAsync(RModel.Email);
                if (emailtest != null)
                {
                    ModelState.AddModelError("", "Email already in use");
                    return Page();
                }
                else
                {
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    RModel.Password = passwordHasher.HashPassword(null, RModel.Password);
                    //sanitize input to prevent XSS
                    RModel.FirstName = HttpUtility.HtmlEncode(RModel.FirstName);
                    RModel.LastName = HttpUtility.HtmlEncode(RModel.LastName);
                    RModel.Email = HttpUtility.HtmlEncode(RModel.Email);
                    RModel.CreditCard = HttpUtility.HtmlEncode(RModel.CreditCard);
                    RModel.PhoneNumber = HttpUtility.HtmlEncode(RModel.PhoneNumber);
                    RModel.BillingAddress = HttpUtility.HtmlEncode(RModel.BillingAddress);
                    RModel.ShippingAddress = HttpUtility.HtmlEncode(RModel.ShippingAddress);
                    RModel.Password = HttpUtility.HtmlEncode(RModel.Password);
                    RModel.ConfirmPassword = HttpUtility.HtmlEncode(RModel.ConfirmPassword);
                    RModel.Photo = HttpUtility.HtmlEncode(RModel.Photo);
                    
                    

                    RijndaelManaged cipher = new RijndaelManaged();
                    ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                    byte[] plainText = Encoding.UTF8.GetBytes(RModel.CreditCard);
                    byte[] cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
                    RModel.CreditCard = Convert.ToBase64String(cipherText);
                    

                    _httpContextAccessor.HttpContext.Session.SetString("FirstName", RModel.FirstName);
                    _httpContextAccessor.HttpContext.Session.SetString("LastName", RModel.LastName);
                    _httpContextAccessor.HttpContext.Session.SetString("Email", RModel.Email);
                    _httpContextAccessor.HttpContext.Session.SetString("CreditCard", RModel.CreditCard);
                    _httpContextAccessor.HttpContext.Session.SetString("PhoneNumber", RModel.PhoneNumber);
                    _httpContextAccessor.HttpContext.Session.SetString("BillingAddress", RModel.BillingAddress);
                    _httpContextAccessor.HttpContext.Session.SetString("ShippingAddress", RModel.ShippingAddress);
                    _httpContextAccessor.HttpContext.Session.SetString("Password", RModel.Password);
                    _httpContextAccessor.HttpContext.Session.SetString("ConfirmPassword", RModel.ConfirmPassword);
                    _httpContextAccessor.HttpContext.Session.SetString("Photo", RModel.Photo);


                    var user = new ApplicationUser()
                    {
                        
                        UserName = RModel.FirstName,
                        Email = RModel.Email,
                        PasswordHash = RModel.Password,
                        PhoneNumber = RModel.PhoneNumber,
                        FirstName = RModel.FirstName,
                        LastName = RModel.LastName,
                        CreditCard = RModel.CreditCard,
                        BillingAddress = RModel.BillingAddress,
                        ShippingAddress = RModel.ShippingAddress,
                        Photo = RModel.Photo

                       
						
				};

                    var result = await userManager.CreateAsync(user, RModel.Password);
                  
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return RedirectToPage("/Index");
                        
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return Page();
        }







    }
}
