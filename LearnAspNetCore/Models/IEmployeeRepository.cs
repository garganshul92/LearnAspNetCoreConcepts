namespace LearnAspNetCore.Models;

public interface IEmployeeRepository
{
    Employee GetEmployee(int id);
    IEnumerable<Employee> GetEmployees();
    Employee AddEmployee(Employee employee);
    Employee UpdateEmployee(Employee employee);
    Employee DeleteEmployee(int id);
}
