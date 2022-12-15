// Account controller was partially scaffolded by Entity Framework, and a good amount of the logic comes from following tutorials.
// There are parts of this class that I don't entirely understand, but I will try to be upfront about those as they come up.

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PrismaPicker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace PrismaPicker.Controllers
{
	public class AccountController : Controller
	{
		// SignInManager and UserManager objects are from the Identity Framework, and are used to check the model state and then log in with the new account
		private SignInManager<Admin> _signInManager;
		private UserManager<Admin> _userManager;

        // Constructor is important for injecting _signInManager and _userManager into views, if I understand correctly.
		// Sign-in operations also don't work without it.
        public AccountController(UserManager<Admin> userManager, SignInManager<Admin> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET Login method records a return url and takes user to login form
        [HttpGet]
		public IActionResult Login()
		{
			ViewBag.Title = "PrismaPicker | Log In";

			// Create new LoginModel object called login
			Login login = new Login { };

			// Pass that object to the view
			return View(login);
		}

		// POST method sends properties of login model created during GET method through a method of SignInManager object to sign user in
		[HttpPost]
		public async Task<IActionResult> Login(Login login)
		{
			if (ModelState.IsValid)
			{
				// variable of type SignInResult 'var' required since type SignInResult can refer to two different things
				var result = await _signInManager.PasswordSignInAsync(login.Username, login.Pass, login.RememberMe, false);

				// Redirect logged in user to /Home/Index view
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
			} 
			else
			{
				// Add error to validation div on /Account/Login view
				ModelState.AddModelError("", "Invalid login attempt");
			}
			return View(login);
		}

		// GET Register method returns the Register view
		// Due to the destructive nature of user accounts for this site, only logged in users can create accounts.
		// That's why it says you should "Talk to an admin" to register for an account on /Account/Login view
		[Authorize]
		[HttpGet]
		public ViewResult Register()
		{
			ViewBag.Title = "PrismaPicker | Register Account";
			return View();
		}

		// POST Register method accepts a Register object, validates it, and then passes it to the sign-in manager to log in the new account-holder
		// Register method is only available to signed in users, due to it's destructive capability
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Register(Register model)
		{
			if (ModelState.IsValid)
			{
				Admin admin = new Admin { UserName = model.Username };
				IdentityResult result = await _userManager.CreateAsync(admin, model.Pass);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(admin, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View();
		}

		// POST Logout method signs the current user out and redirects them to the home-page
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			// Uses another SignInManager method to handle the user logging out
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
