using Microsoft.EntityFrameworkCore;
using YatraBackend.Database.Models;

namespace YatraBackend.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    
    
    // seeding pre-required data to the database
    // these data will be available whenever the database is created without having user to add them manually
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Name = "Admin", Description = "Administrative roles and permissions" },
            new Role { Name = "User", Description = "Basic user roles and permissions" }
        );

        // modelBuilder.Entity<Field>().HasData(
        //     new Field { Name = "Health" },
        //     new Field { Name = "Education" },
        //     new Field { Name = "Tourism" },
        //     new Field { Name = "Transport" },
        //     new Field { Name = "Finance" },
        //     new Field { Name = "Agriculture" },
        //     new Field { Name = "Fashion" },
        //     new Field { Name = "Social Media" },
        //     new Field { Name = "E-commerce" }
        // );

        base.OnModelCreating(modelBuilder);
    }
}