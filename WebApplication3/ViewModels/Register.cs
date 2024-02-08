using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {
		[Required]
		[DataType(DataType.Text)]
		[StringLength(50, MinimumLength = 2)]
		public string FirstName { get; set; }
		[Required]
		[DataType(DataType.Text)]
		[StringLength(50, MinimumLength = 2)]
		public string LastName { get; set; }
		
		[Required]
		[DataType(DataType.CreditCard)]
		[RegularExpression(@"^(\d{16})$", ErrorMessage = "Invalid Credit Card Number")]
		public string CreditCard { get; set; }


		[Required]
		[DataType(DataType.PhoneNumber)]
		//regex to not allow letters 
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Phone Number")]
		public string PhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[RegularExpression(@"^[a-zA-Z0-9\s,]*$", ErrorMessage = "Only alphanumeric characters allowed.")]
		public string BillingAddress { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string ShippingAddress { get; set; }

		[Required]
        [DataType(DataType.EmailAddress)]
		[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one digit and one special character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
		
		[DataType(DataType.Upload)]
		[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg)$", ErrorMessage = "Only jpg files allowed.")]
		public string Photo { get; set; }


	}
}
