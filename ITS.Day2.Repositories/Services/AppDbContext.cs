using ITS.Day2.Models;
using ITS.Day2.Repositories.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ITS.Day2.Repositories.Services
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ProductConfiguration());
        }
    }
}
