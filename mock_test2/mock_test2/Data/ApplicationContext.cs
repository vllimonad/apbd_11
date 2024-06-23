using Microsoft.EntityFrameworkCore;
using mock_test2.Models;

namespace mock_test2.Data;

public class ApplicationContext: DbContext
{
    protected ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Pastry> Pastries { get; set; }
    public DbSet<Order_Pastry> OrderPastries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Client>().HasData(new List<Client>()
        {
            new() { ID = 1, FirstName = "John", LastName = "Doe"},
            new() { ID = 2, FirstName = "Ann", LastName = "Smith"},
            new() { ID = 3, FirstName = "Jack", LastName = "Taylor"}
        });
        
        modelBuilder.Entity<Employee>().HasData(new List<Employee>()
        {
            new() { ID = 1, FirstName = "John", LastName = "Doe"},
            new() { ID = 2, FirstName = "Ann", LastName = "Smith"},
            new() { ID = 3, FirstName = "Jack", LastName = "Taylor"}
        });
        
        modelBuilder.Entity<Order>().HasData(new List<Order>()
        {
            new() { ID = 1, AcceptedAt = DateTime.Today, FulfilledAt = DateTime.Parse("2024-03-03"), Comments = "IBYIDY", ClientId = 1, EmployeeId = 1},
            new() { ID = 2, AcceptedAt = DateTime.Today, FulfilledAt = null, Comments = "IDBD", ClientId = 1, EmployeeId = 2},
            new() { ID = 3, AcceptedAt = DateTime.Today, FulfilledAt = null, Comments = null, ClientId = 2, EmployeeId = 3}
        });
        
        modelBuilder.Entity<Pastry>().HasData(new List<Pastry>()
        {
            new() { ID = 1, Name = "etynewy", Price = 42.1M, Type = "A"},
            new() { ID = 2, Name = "qetbg", Price = 93.4M, Type = "B"},
            new() { ID = 3, Name = "Jweg4tytack", Price = 7.7M, Type = "C"}
        });
        
        modelBuilder.Entity<Order_Pastry>().HasData(new List<Order_Pastry>()
        {
            new() { OrderId = 1, PastryId = 1, Comments = "Good", Amount = 42},
            new() { OrderId = 1, PastryId = 2, Comments = null, Amount = 132},
            new() { OrderId = 2, PastryId = 1, Comments = null, Amount = 7},
            new() { OrderId = 3, PastryId = 3, Comments = null, Amount = 7},
            new() { OrderId = 2, PastryId = 3, Comments = "null", Amount = 7}
        });
        
    }
}