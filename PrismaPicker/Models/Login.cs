using System.ComponentModel.DataAnnotations;

// Login model contains data relevant to a user-login view
namespace PrismaPicker.Models
{
	public class Login
	{
		// Username
		public string Username { get; set; }

		// Passowrd
		[DataType(DataType.Password)]
		public string Pass { get; set; }

		// RememberMe is not editable in any way from the webapp itself. I would remove it, but login breaks without it
		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }
	}
}
