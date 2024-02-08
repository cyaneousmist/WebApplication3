using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplication3.Pages;
using WebApplication3.ViewModels;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(Register model);
    Task<ApplicationUser> FindByEmailAsync(string email);

}

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(Register model)
    {

        var user = new ApplicationUser
        {
            UserName = model.FirstName,
            Email = model.Email,
            PasswordHash = model.Password, 
            PhoneNumber = model.PhoneNumber,
            
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
        }

        return result;
    }

    public async Task<ApplicationUser> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    // Add other user-related methods as needed
}