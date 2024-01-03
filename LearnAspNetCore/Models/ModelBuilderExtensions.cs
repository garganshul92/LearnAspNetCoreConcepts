using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace LearnAspNetCore.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
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
}
