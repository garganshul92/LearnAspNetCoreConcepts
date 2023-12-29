namespace LearnAspNetCore.Models;

public interface IEmployeeRepository
{
    Employee GetEmployee(int id);
    IEnumerable<Employee> GetEmployees();
}
