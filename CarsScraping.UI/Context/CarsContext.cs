using CarsScrape.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsScraping.UI.Context
{
    public class CarsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=localhost, 11433;database=CarsScrapingDB;uid=sa;Pwd=12345678!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Cars");
            });
        }

        public DbSet<Car> Cars { get; set; }
    }
}
