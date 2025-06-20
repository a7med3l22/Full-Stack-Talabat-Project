using System.ComponentModel.DataAnnotations;

namespace Talabat_APIs.Dto.Identity
{
	public class RegisterDto
	{
		public string DisplayName { get; set; }= null!;
		[EmailAddress]
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		[Compare("Password",ErrorMessage = "Password and confirmation password do not match")]
		public string ConfirmPassword { get; set; } = null!;
		[Phone]
		public string phoneNumber { get; set; } = null!;
		public AddressDto? Address { get; set; }



	}
}
