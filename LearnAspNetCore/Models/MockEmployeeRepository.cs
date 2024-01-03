
namespace LearnAspNetCore.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;
        public MockEmployeeRepository()
        {
            _employees = new List<Employee>() {
                new Employee { Id = 1, Name = "Ayush", Department=Dept.IT, Email="ayush@test.com" },
                new Employee { Id = 2, Name = "Nikhil", Department=Dept.HR, Email="nikhil@test.com" },
                new Employee { Id = 3, Name = "Anshul", Department=Dept.IT, Email="anshul@test.com" },
            };
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = _employees.Max(x => x.Id) + 1;
            _employees.Add(employee);
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                _employees.Remove(employee);
            }

            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employees;
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == employeeChanges.Id);

            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }

            return employee;
        }
    }
}
