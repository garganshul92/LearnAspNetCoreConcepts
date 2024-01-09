
namespace LearnAspNetCore.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SQLEmployeeRepository> logger;

        public SQLEmployeeRepository(AppDbContext context, ILogger<SQLEmployeeRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }
        public Employee AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;

        }

        public Employee DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

            if(employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return employee;
        }

        public Employee GetEmployee(int id)
        {

            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Info Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            var employee = _context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeChanges;
        }
    }
}
