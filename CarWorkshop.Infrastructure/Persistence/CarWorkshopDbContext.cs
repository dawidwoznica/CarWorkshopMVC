using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence
{
    using Domain.Entities;

    public class CarWorkshopDbContext(DbContextOptions options) : IdentityDbContext(options)
    {
        public DbSet<CarWorkshop> CarWorkshops { get; set; }
        public DbSet<CarWorkshopService> Service { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarWorkshop>().OwnsOne(c => c.ContactDetails);

            modelBuilder.Entity<CarWorkshop>().HasMany(c => c.Services)
                .WithOne(s => s.CarWorkshop)
                .HasForeignKey(s => s.CarWorkshopId);
        }
    }
}
