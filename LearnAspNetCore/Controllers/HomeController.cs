using LearnAspNetCore.Models;
using LearnAspNetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearnAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        // A good practice as 
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public ViewResult Index()
        {
            var model = _employeeRepository.GetEmployees();
            return View(model);
        }

        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel() {
                PageTitle = "Employee Details",
                Employee = model
            };
            return View(viewModel);
        }
    }
}
