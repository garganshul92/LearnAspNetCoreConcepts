namespace LearnAspNetCore.Models;

public interface IEmployeeRepository
{
    Employee GetEmployee(int id);
}
