using Microsoft.AspNetCore.Identity;

namespace PrismaPicker.Models
{
	// Admin class is the basic user role. It extends IdentityUser from the Microsoft Identity Framework
	// It stores a variety of fields for users which you can use as many or few of as you want.
	public class Admin : IdentityUser
	{
	}
}
