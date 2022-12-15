// Entity Framework DbContext class. I don't remember at this point if this was scaffolded or not. I think it was though.

using Microsoft.EntityFrameworkCore;
using PrismaPicker.Models;

namespace PrismaPicker.Data
{
    public class PrismaPickerContext : DbContext
    {
        public PrismaPickerContext(DbContextOptions<PrismaPickerContext> options) : base(options)
        {
        }

        public DbSet<Glass> Glass { get; set; }
    }
}
