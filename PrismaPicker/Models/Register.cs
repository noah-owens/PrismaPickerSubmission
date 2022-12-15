using System.ComponentModel.DataAnnotations;

// Register model contains data relevant to the Account/Register view

namespace PrismaPicker.Models
{
	public class Register
	{
		// Username
		[Required, MaxLength(20)]
		public string Username { get; set; }

		// Password 
		[Required, DataType(DataType.Password)]
		public string Pass { get; set; }

		// Password confirmation
		[Required, DataType(DataType.Password), Compare(nameof(Pass))]
		public string ConfirmPass { get; set; }
	}
}
