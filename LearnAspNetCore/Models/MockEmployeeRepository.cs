
namespace LearnAspNetCore.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;
        public MockEmployeeRepository()
        {
            _employees = new List<Employee>() { 
                new Employee { Id = 1, Name = "Ayush", Department="IT", Email="ayush@test.com" },
                new Employee { Id = 2, Name = "Nikhil", Department="HR", Email="nikhil@test.com" },
                new Employee { Id = 3, Name = "Anshul", Department="IT", Email="anshul@test.com" },
            };
        }
        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employees;
        }
    }
}
