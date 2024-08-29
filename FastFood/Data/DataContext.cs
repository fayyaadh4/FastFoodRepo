using FastFood.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

    }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles {  get; set; }
        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(l => l.Location);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.MenuItems)
                .WithOne()
                .HasForeignKey(m => m.RestaurantId);


            // one to many
            // one restaurant can have any employees
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Employees)
                .WithOne()
                .HasForeignKey(e => e.RestaurantId);

            // One to one
            // one employee can have one leave
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.EmployeeLeave);


            // One to one
            // one employee can have one location
            modelBuilder.Entity<Employee>()
                .OwnsOne(l => l.Location);

            // one to many
            // one role can have many employees
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Employees)
                .WithOne()
                .HasForeignKey(e => e.RoleId);

        }

    }
}
