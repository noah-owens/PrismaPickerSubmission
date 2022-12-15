// Startup.cs inspired by sample Startup class on page 49 of Murach's ASP.NET Core MVC

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrismaPicker.Data;
using PrismaPicker.Models;

namespace PrismaPicker
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
        {
			_configuration = configuration;
        }

		// Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddDbContext<PrismaPickerContext>(options =>
			options.UseNpgsql(_configuration.GetConnectionString("PrismaPickerContext")));

			services.AddDbContext<AdminContext>(options =>
			options.UseNpgsql(_configuration.GetConnectionString("PrismaPickerContext")));

			services.AddIdentity<Admin, IdentityRole>()
				.AddEntityFrameworkStores<AdminContext>();
		}

		// Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			// Authentication and Authorization *MUST BE* in this order, and between UseRouting() and UseEndpoints().
			// It won't work any other way
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
