// Like with AccountController, the whole identity system and login/out operations was difficult for me to wrap my head around
// I learned this from first trying to replicate my other DBContext class PrismaPickerContext, and running into a bunch of issues that I had to look up

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrismaPicker.Models;

namespace PrismaPicker.Data
{
	// AdminContext class is a DbContext which extends a more specific class in the Microsoft Idendity Framework for dbContexts
	public class AdminContext : IdentityDbContext<Admin>
	{
		// Empty constructor
		public AdminContext(DbContextOptions<AdminContext> options) : base(options)
		{
		}

		// Create an Identity DBSet
		public DbSet<Admin> Admins { get; set; }
	}
}
