using Microsoft.EntityFrameworkCore;
using SnlMaps.Domain;

namespace SnlMaps.Web.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasKey(city => city.InseeCode);
            modelBuilder.Entity<City>().Property(city => city.Name);
            modelBuilder.Entity<City>().Property(city => city.Population);
            modelBuilder.Entity<City>().Property(city => city.Geometry);
            modelBuilder.Entity<City>().Property(city => city.PostCode);
            modelBuilder.Entity<City>().Property(city => city.Location);
            modelBuilder.Entity<City>().Property(city => city.SruDeficit);
            modelBuilder.Entity<City>().Property(city => city.SocialHousingRate);
            modelBuilder.Entity<City>().Property(city => city.SocialHousingCount);
            modelBuilder.Entity<City>().Property(city => city.SnlHousingCount);
        }
    }
}