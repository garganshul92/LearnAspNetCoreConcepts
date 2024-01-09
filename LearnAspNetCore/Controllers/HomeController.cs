using LearnAspNetCore.Models;
using LearnAspNetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearnAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        // A good practice as 
        private readonly IEmployeeRepository _employeeRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly ILogger<HomeController> logger;

        public HomeController(IEmployeeRepository employeeRepository, 
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        public ViewResult Index()
        {
            //throw new Exception("Intentionally thrown an exception");
            var model = _employeeRepository.GetEmployees();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Info Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            Employee model = _employeeRepository.GetEmployee(id.Value);

            if(model == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                PageTitle = "Employee Details",
                Employee = model
            };
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                var newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName,
                };

                _employeeRepository.AddEmployee(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            var employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        var existingFilePath = Path.Combine(hostingEnvironment.WebRootPath, 
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(existingFilePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction("details", new { id = employee.Id });
            }

            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                model.Photo.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
