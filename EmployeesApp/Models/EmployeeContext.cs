using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Models;

public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasData
            (
             new Employee
             {
                 Id = Guid.Parse("3c904a3a-91eb-4d3c-b332-b1315d929875"),
                 Name = "Mark",
                 AccountNumber = "123-3452134543-32",
                 Age = 30
             },
             new Employee
             {
                 Id = Guid.Parse("081ffa8e-32c4-431b-a450-aa4ede79eb43"),
                 Name = "Evelin",
                 AccountNumber = "123-9384613085-55",
                 Age = 28
             }
            );
    }

    public DbSet<Employee>? Employees { get; set; }
}