﻿using System.ComponentModel.DataAnnotations;
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
		public string CreditCard { get; set; }


		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string BillingAddress { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string ShippingAddress { get; set; }

		[Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

		[DataType(DataType.Upload)]
		public IFormFile Photo { get; set; }


	}
}
