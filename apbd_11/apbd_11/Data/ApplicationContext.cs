using apbd_11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_11.Context;

public class ApplicationContext : DbContext
{
    protected ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>().HasData(new List<ApplicationUser>()
        {
            new() { Id = 1, Login = "qqq", Password = "1234", RefreshToken = "", RefreshTokenExp = DateTime.Parse("2024-10-10"), Salt = ""},
            new() { Id = 2, Login = "www", Password = "12345", RefreshToken = "", RefreshTokenExp = DateTime.Parse("2024-10-10"), Salt = ""},
            new() { Id = 3, Login = "eee", Password = "12346", RefreshToken = "", RefreshTokenExp = DateTime.Parse("2024-10-10"), Salt = ""},
        });
    }
}