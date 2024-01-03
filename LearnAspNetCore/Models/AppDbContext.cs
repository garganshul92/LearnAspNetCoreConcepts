using Microsoft.EntityFrameworkCore;

namespace LearnAspNetCore.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
          new Employee
          {
              Id = 1,
              Name = "Marry",
              Department = Dept.IT,
              Email = "marry@learnaspnetcore.com"
          },
          new Employee
          {
              Id = 2,
              Name = "Jone",
              Department = Dept.HR,
              Email = "john@learnaspnetcore.com"
          });
    }
}
