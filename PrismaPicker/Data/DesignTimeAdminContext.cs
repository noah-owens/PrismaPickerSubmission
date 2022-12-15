// Pulled from stackoverflow https://stackoverflow.com/questions/64676058/entity-framework-core-no-design-time-services-were-found
// This was a solution to a persistent issue I was having where I could create migrations, but 'dotnet-ef database update' wasn't creating the identity tables that I needed it to make. 

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PrismaPicker.Data
{
    public class DesignTimeAdminContext : IDesignTimeDbContextFactory<AdminContext>
    {
        public AdminContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdminContext>();
            
            // Hardcoded connection string put here, since the one I stored in appsettings.json wasn't getting used right by AdminContext
            optionsBuilder.UseNpgsql("SAMPLE CONNECTION STRING");

            return new AdminContext(optionsBuilder.Options);
        }
    }
}