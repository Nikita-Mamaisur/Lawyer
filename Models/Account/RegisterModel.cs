using System.ComponentModel.DataAnnotations;


namespace Lawyer.Models.Account
{
	public class RegisterModel
	{
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6)]
		public string Password { get; set; }

		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}