using Microsoft.EntityFrameworkCore;
using YatraBackend.Database.Models;

namespace YatraBackend.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Domain> Domains => Set<Domain>();
    public DbSet<Project> Projects => Set<Project>();
    
    
    // seeding pre-required data to the database
    // these data will be available whenever the database is created without having user to add them manually
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Name = "Admin", Description = "Administrative roles and permissions" },
            new Role { Name = "User", Description = "Basic user roles and permissions" }
        );

        modelBuilder.Entity<Domain>().HasData(
            new Domain { Name = "Health" },
            new Domain { Name = "Education" },
            new Domain { Name = "Tourism" },
            new Domain { Name = "Transport" },
            new Domain { Name = "Finance" },
            new Domain { Name = "Agriculture" },
            new Domain { Name = "Fashion" },
            new Domain { Name = "Social Media" },
            new Domain { Name = "E-commerce" }
        );

        base.OnModelCreating(modelBuilder);
    }
}