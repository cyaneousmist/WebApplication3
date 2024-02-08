using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class Login
    {

        [Required]
        [DataType(DataType.EmailAddress)]
		[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
		public string Email { get; set; }
		
		[Required]
        [DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one digit and one special character")]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

