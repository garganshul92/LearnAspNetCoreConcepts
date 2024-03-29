﻿## 4. ASP NET core Project Files
- Program.cs
- Startup.cs
- asssettings.json
- launchSettings.json

## 5. Main Method in C#
- Entry point of the application
- Setup Config Settings
- Inject Dependencies
- Setup Middleware
- CreateWebHost setup the Environment(e.g. Inprocess => IIS, IIS Express OR OutProcess => Kestrel, IIS, Apache Cordva etc.)

## 6. ASPNET Core Inprocess Hosting
- Hosting on IIS/IIS Express
    - Faster as there is communication between Kesteral(dotnet.exe) and External Servers.

## 7. SPNET core OutProcess Hosting
- External Servers work as Reverse Proxy (Clients interact with Reverse Proy and Reverse Proxy communicate with Kesteral server).

## 8. ASPNET Core launchSettings.json

## 9. ASPNET Core appsettings.json
- Appsettings contains the config info
- Using IConfiguration, we can read the config files
- Order of Configuration (Later config sources override previous config source)
    - AppSetting.json
    - AppSettings.{Environment}.json
    - User Secret
    - Environment Variables (in launchSettings.json)
    - Command Line Arguments

## 10. ASPNET Core Middleware
- Request Pipeline
    - Middleware for Authentication
    - Middleware for Serving Static files and so on

- Configure method can setup our middleware pipeline
- Middleware can decide to not allow the request to move ahead and this is called Short Circuiting
- Middleware have access to Request as well as Response and can update both Request as well as Response
- Middleware execute in the same order as they are defined.

## 11. Configure ASP NET core request pipeline
- app.Run is terminal middleware
- Middlewares after app.Run will not run
- next() run the next Middleware

```
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello from First");
    await next();
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from Second");
});
```


## 12. Static files in ASP NET core
- All the static files are served from wwwroot folder
- Add app.UseStaticFiles() middleware to serve static files
- To server default file, we need to add app.UseDefaultFiles() middleware
- Order of middleware is important, UseDefaultFiles() should be before UseStaticFiles()
- To configure default file, we need to add DefaultFilesOptions in Configure method
```
    DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("staticHtmlFile.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
```
- app.UseFileServer() is combination of UseDefaultFiles() and UseStaticFiles()
- To serve files from other folder, we need to add FileServerOptions in Configure method
```
FileServerOptions options = new FileServerOptions();
options.DefaultFilesOptions.DefaultFileNames.Clear();
options.DefaultFilesOptions.DefaultFileNames.Add("staticHtmlFile.html");
app.UseFileServer(options);
```

## 13. ASP.NET Core Developer Exception Page
- Developer Exception Page is only for Development Environment
- To enable Developer Exception Page, we need to add app.UseDeveloperExceptionPage() middleware
- Must be plugged as early as possible- 
- Contains useful information like Stack Trace, Query String, Cookies, Headers, Route Data, Environment Variables, Connection Strings, Loaded Assemblies, Loaded Types, Loaded Services, Request Body, Response Body, Session, Cache, Claims, Identity, Environment Variables, Request and Response Headers, Cookies, Query String, Route Data, Connection Strings, Loaded Assemblies, Loaded Types, Loaded Services, Request Body, Response Body, Session, Cache, Claims, Identity
- Use DeveloperPageException options to configure the developer exception page
```
if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    DeveloperExceptionPageOptions options = new();
    options.SourceCodeLineCount = 10;
    app.UseDeveloperExceptionPage(options);
}
```

## 14. ASP.NET Core Environment Variables
- ASPNETCORE_ENVIRONMENT is the environment variable    
- Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") will return the environment variable value
- Environment variables can be set in launchSettings.json
```
"profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5008",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
```
- Environment variables can be set in Visual Studio
	- Project Properties -> Debug -> Environment Variables
- Environment variable can be set via Environment variable in window
- Environment variable set in launchSettings.json will override the environenment variable set in window.
- Use IHostingEnvironment to get the environment name
- Runtime environment defaut value is Production
- In addition to standard environment variable, we can also create custom environment variable

## 15. ASP.NET Core MVC Tutorial
- MVC is an architectural pattern for implementing user interface layer of an application
- Model - Set of Classes that represent the data + the logic to manage the data
- View - Contains the UI logic to display the Model Data provided to it by the Controller
- Controller - Handles the HTTP request, work with the Model, and selects a View to render that Model data.

## 16. ASP.NET Core MVC setup the MVC in ASP.NET Core
- Step 1: Add the MVC services to the Dependency injection Container
```
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
```

- Add MVC Middleware to the Request Pipeline
```
app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
app.Run();
```

## 17. ASP.NET Core AddMvc vs AddMvcCore
- AddMvcCore() method only adds the core MVC services
- AddMvc() method adds all the required MVC services.
- AddMvc() method internally calls AddMvcCore() method.

## 18. Model in ASP.NET Core MVC
- In ASP.NET core, it is not mandatory to keep Controllers, Models in respective folders. They can live in any folder.
- Model = Set of classes that represent the data and the logic to manage the data (e.g. Employee, IEmployeeRepository, EmployeeRepository)

## 19. ASP.NET Core Dependency Injection
- To Register with Dependency Injection Container, ASP.NET provides us 3 methods
    - AddSingleton() - Single instance for service
    - AddTransient() - New Instance everytime
    - AddScoped() - New Instance created per HTTP request
- Benefits of DI
    - Loosely coupled
    - Easy to Unit test
- It is a good practice to make the private field as readonly, as it helps us avoiding the assignment of the field new value in the class.

## 20. Controller in ASP.NET Core MVC
- A class that derives from Controller Base class.
- Usually Controller class names end with Controller (e.g HomeController, EmployeeController etc.).
- Controller handles the incoming HTTP request
- Builds the Model AND
- Returns the Model data to the caller if we are building an API OR
- Select a View and pass the model data to the view
- The view generates the required HTML to present the data.

## 21. View in ASP.NET Core MVC
- A view file have .cshtml file extension
- A view is an html template with embedded Razor markup
- Contains logic to display the data
- All the views belonging to controller should be in the controller name folder in views folder
- We can also change the default folder conventions too.

## 22. Customize view discovery in ASP.NET Core MVC
- View() or View(object model): Looks for a view with file name same as action method in controller name folder in views folder.
- View(string viewName)
    - Looks for a view file with your own custom name
    - You can specify  a view name or a view file path
    - View file path can be relative or absolute
    - With absolute file path .cshtml extension must be specified.
    ```
       return View("MyViews/TestMyView.cshtml", model);
    ```
    - With relative file path, we don't specify the .cshtml extension.
    ```
       return View("../Test/Test", model);
    ```
- View(string viewName, object model)

## 23. Passing data to view in ASP.NET Core MVC
- Three ways to pass data to View
    - ViewData
    - ViewBag
    - Strongly Typed View
- ViewData
    - Dictionary of weekly typed objects
    - Use string keys to store and retrieve the data
    - Dynamically resolved at run time
- ViewData Drawbacks
    - We can't determine the errors at compile time as this is loosely typed
- Controller Code
    ```
        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee Details";
            return View();
        }
    ```
- View Code
    ```
        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <title></title>
            </head>
            <body>
                <h3>@ViewData["PageTitle"]</h3>

                @{
                    var employee = ViewData["Employee"] as LearnAspNetCore.Models.Employee;
                }
                <div>
                    Name: @employee.Name
                </div>
                <div>
                    Email: @employee.Email
                </div>
                <div>
                    Department: @employee.Name
                </div>
            </body>
            </html>
    ``` 

## 24. ViewBag in ASP.NET Core MVC
- ViewBag is wrapper around ViewData
- ViewBag uses Dynamic properties
- Creates a loosely typed view
- Resolved dynamically at runtime
- No compile time type checking and intellisense
- Preferred approach to pass data from Controller to view is Strongly Typed View
- Controller Code
    ```
        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            ViewBag.Employee = model;
            ViewBag.PageTitle = "Employee Details";
            return View();
        }
    ```  
- View Code
    ```
        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <title></title>
            </head>
            <body>
                <h3>@ViewBag.PageTitle</h3>

                <div>
                    Name: @ViewBag.Employee.Name
                </div>
                <div>
                    Email: @ViewBag.Employee.Email
                </div>
                <div>
                    Department: @ViewBag.Employee.Department
                </div>
            </body>
            </html>
    ```

## 25. Strongly Typed View in ASP.NET Core MVC
- Specify the model type in the view using @model Directive
    ```
    @model LearnAspNetCore.Models.Employee;
    ```
- To access the model properties we use @Model
    ```
        <div>
            Name: @Model.Name
        </div>
        <div>
            Email: @Model.Email
        </div>
    ```
- It is always advisable to avoid ViewBag and ViewData as we don't get compile time error in case of misspelled properties.

## 26. ViewModel in ASP.NET Core MVC
- We create a ViewModel when our model object doesn't contain all the data our view needs
    ```
        public class HomeDetailsViewModel
        {
            public string PageTitle { get; set; }
            public Employee Employee { get; set; }
        }
    ```
    ```
        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel() {
                PageTitle = "Employee Details",
                Employee = model
            };
            return View(viewModel);
        }
    ```

## 27.List View in ASP.NET Core MVC
- We can use foreach loop in html to populate the List View
    ```
    @foreach(var employee in Model)
    {
        <tr>
            <td>
                @employee.Id
            </td>
            <td>
                @employee.Name
            </td>
            <td>
                @employee.Department
            </td>
        </tr>
    }
    ```

## 28. Layout View in ASP.NET Core MVC
- Provides consistent look and behaviour for all the views in a web application
- Similar to ASP.NET master page in ASP.NET Webforms
- File on file system with extension .cshtml
- Default name is _Layout.cshtml but we are free to use any name.
- Code in Layout file
    ```
        <!DOCTYPE html>

        <html>
        <head>
            <meta name="viewport" content="width=device-width" />
            <title>@ViewBag.Title</title>
        </head>
        <body>
            @RenderBody()
        </body>
        </html>
    ```
- Code in View files to refer Layout file
    ```
        @{
            Layout = "~/Views/Shared/_Layout.cshtml";
            ViewBag.Title = "Employees List";
        }
    ```

## 29. Sections in Layout Page in ASP.NET Core MVC
- A Section in a Layout View provides a way to organize where certain page elements should be placed
- A Section can be optional or manadatory
- A Section in the Layout view  is rendered at the location where RenderSection() method is called
    ```
    <!DOCTYPE html>

    <html>
    <head>
        <meta name="viewport" content="width=device-width" />
        <title>@ViewBag.Title</title>
    </head>
    <body>
        @RenderBody()
    </body>
    @if (IsSectionDefined("Scripts"))
    {
        @RenderSection("Scripts", required: true)
    }
    </html>
    ```

## 30. _ViewStart.cshtml file in ASP.NET Core MVC
- Code in ViewStart is executed before the code in an individual file.
- Move the **common code** such as setting the Layout property to ViewStart
- ViewStart reduces **code redundancy** and **improves maintainability**
- ViewStart is hierarchical

## 31. _ViewImports.cshtml in ASP.NET Core MVC
- _ViewImports file is placed in Views folder
- Used to include common **NameSpaces**
- To include common namespaces we use **@using** directive
- Other supported directives
    - @addTagHelper
    - @removeTagHelper
    - @tagHelperPrefix
    - @model
    - @inherits
    - @injects
- _ViewImports file is also hierarchical

## 32. Routing in ASP.NET Core MVC
- Routing Techniques
    - Conventional Routing
    - Attribute Routing
- In Conventional routing, we have 3 parts in URL (e.g. http://localhost:5008/home/details/2) 
    - First part maps to Controller (e.g. home in above example)
    - Second parts maps to Action method (e.g. details in above example)
    - Third part can be optional for model binding (e.g 2 in above example)
- We can configure routing using **app.UseMvcWithDefaultRoute();**
- Or by using **app.UseMVC()** for custom routes
    ```
        app.UseMvc(routes =>
        {
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });
    ```

## 33. Attribut Routing in ASP.NET Core MVC
- With Attribute Routing, [Route] attribute is used to define the Routes
- Route can be applied on the Controller or the Controller Action methods
- With attribute routing, routes are placed next to the action methods that will actually use them
- Attribute routing offers more flexibility than concentional routes
- Attribute Routes are Hierarchichal
    - **Note** - The controller route template is not combined with the action method route template if the route template on action method start with "/" ot "~/"
- Examples for Attribute Routing
    - [Route("Home/Index")] on action method
    - [Route["Home"] on controller and Route["Index"] on action method
    - Using Tokens in Attribute Routing
        - [Route("[controller]/[action]")]
        - [Route("[controller]")] and [Route("[action]")] separately on controller as well as action method
    - Use Route["/"] or Route["~/"] for default route

## 34. Install and use Bootstrap in ASP.NET Core MVC 
- Tools to install Client-Side packages
    - Bower
    - NPM
    - WebPack
    - LibMan (Default in VS)
- Library Manager (LibMan)
    - Light-weight, client side library aquisition tool
    - Downloads from the file system or from a CDN
    - libman.json is the library manager manifest file
    - Use GUI or libman.json file to manage the packages 
    
## 35. Tag helper in ASP.NET Core MVC
- Server side components
- Processed on the server side to create and render HTML elements
- Similar to HTML Helpers
- Built-in tag helpers - Generating links, creating forms and loading assets etc.
- Importing tag helpers - @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
- Usage
    ```
        <a asp-controller="home"
                   asp-action="details"
                   asp-route-id="@employee.Id"
                   class="btn btn-primary"> View </a>
    ```

## 36. Why use Tag Helpers
- Tag helpers bind the Urls based on Route Template defined in app.UseMvc() middleware. So, if we change something there links will be adjusted automatically.
- If we don't use tag helpers and direct links, then those links will start failing once we update the Route template.

## 37. ASP.NET Core Image Tag Helper
- Image tag helper enhaces the <img> tag to provide cache-busting behaviour for static files
    ``` <img class="card-img-top" src="~/images/noimage.jpg" asp-append-version="true"/> ``` 
- Based on the content of the image, a hash is generated and appended with the image url.
    ``` <img class="card-img-top" src="/images/noimage.jpg?v=RmsuiLbBLMR_XWkV9f-BMkToyr19IK4QcG0IHC_KDKY" /> ``` 
- Each time an image is changed on the server side, a new hash is generated and cached

## 38. ASP.NET Core Environment Tag Helper
- The application environment name is set using APNECTCORE_ENVIRONMENT
- Environment tag helper supports rendering different content depending on the application environment
    ```
        <environment include="Development">
            <link href="~/lib/bootstrap/css/bootstrap.css" rel="stylesheet" />
        </environment>
        <environment include="Staging, Production">
            <link href="https://CDN URL/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Integrity_Hash.... />
        </environment>
        <environment exclude="Development">
            <link href="https://CDN URL/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Integrity_Hash.... />
        </environment>
    ```
- We can use include and/or exclude attribute
- SubResource Integrity (SRI)
    - The "integrity" attribute on the <link> element is used for SubResource Integrity Check
    - SRI is security feature that allows browser to check if the file being retrieved has been maliciously altered
- CDN and Fallback Source
    ```
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"
                  integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"
                  asp-fallback-href="~/lib/bootstrap/css/bootstrap.css"
                  asp-fallback-test-class="sr-only"
                  asp-fallback-test-property="position"
                  asp-fallback-test-value="absolute"
                  asp-suppress-fallback-integrity="true">
    ```
    - Use asp-fallback-href attribute to specify a fallback source
    - Use asp-suppress-fallback-integrity attribute to suppress the intgrtiy check if the file is downloaded from the fallback source

## 39. Bootstrap Navigation Menu in ASP.NET Core
```
<body>
    <div class="container">
        <nav class="navbar navbar-expand-sm bg-dark navbar-dark">
            <a class="navbar-brand" asp-controller="home" asp-action="index">
                <img src="~/images/employees.png" height="30" width="30" />
            </a>
            <button type="button" class="navbar-toggler" data-toggle="collapse" data-target="#collapsibleNavbar">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="collapsibleNavbar">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a asp-controller="home" asp-action="index" class="nav-link">List</a>
                    </li>
                    <li class="nav-item">
                        <a asp-controller="home" asp-action="index" class="nav-link">Create</a>
                    </li>
                </ul>
            </div>
        </nav>
        <div class="container">
            @RenderBody()
        </div>
    </div>
</body>
```

## 40. Form Tag Helper in ASP.NET Core MVC
```
@model Employee;

@{
    ViewBag.Title = "Create Employee";
}

<form asp-controller="home" asp-action="create" method="post" class="mt-3">
    <div class="form-group row">
        <label asp-for="Name" class="col-sm-2 col-form-label"></label>
        <div class="col-sm-10">
            <input asp-for="Name" class="form-control" placeholder="Name" />
        </div>
    </div>
    <div class="form-group row">
        <label asp-for="Email" class="col-sm-2 col-form-label"></label>
        <div class="col-sm-10">
            <input asp-for="Email" class="form-control" placeholder="Email" />
        </div>
    </div>
    <div class="form-group row">
        <label asp-for="Department" class="col-sm-2 col-form-label"></label>
        <div class="col-sm-10">
            <select asp-for="Department" class="form-select col-sm-2"
                    asp-items="Html.GetEnumSelectList<Dept>()"></select>
        </div>
    </div>
    <div>
        <button type="submit">Create</button>
    </div>
</form>
```

## 41. ASP.NET Core Model Binding
- Model Binding in ASP.NET Core MVC is based on Name of the properties.

## 42. ASP.NET Core Model Validation
- Built-In Validation Attributes
    - RegularExpression
    - MinLength
    - MaxLength
    - Required
    - Range
    - Compare
- Step 1. Apply Validation Attribute(s) on Properties
    ```
        [Required]
        [MaxLength(50, ErrorMessage = "Name can't exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-93](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid Email Format")]
        [Display(Name="Official Email")]
        public string Email { get; set; }
    ```
- Step 2. Use ModelState.IsValid property to check if validation has failed or succeded
    ```
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = _employeeRepository.AddEmployee(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }
    ```
- Step 3. Use **asp-validation-for** and **asp-validation-summary** tag helpers to display validation erros
    ```
        <form asp-controller="home" asp-action="create" method="post" class="mt-3">
            <div class="form-group row">
                <label asp-for="Name" class="col-sm-2 col-form-label"></label>
                <div class="col-sm-10">
                    <input asp-for="Name" class="form-control" placeholder="Name" />
                    <span asp-validation-for="Name" class="text-danger"></span>
                </div>
            </div>
            <div class="form-group row">
                <label asp-for="Email" class="col-sm-2 col-form-label"></label>
                <div class="col-sm-10">
                    <input asp-for="Email" class="form-control" placeholder="Email" />
                    <span asp-validation-for="Email" class="text-danger"></span>
                </div>
            </div>
            <div class="form-group row">
                <label asp-for="Department" class="col-sm-2 col-form-label"></label>
                <div class="col-sm-10">
                    <select asp-for="Department" class="form-select col-sm-2"
                            asp-items="Html.GetEnumSelectList<Dept>()"></select>
                </div>
            </div>
            <div asp-validation-summary="All" class="text-danger">
            </div>

            <div>
                <button type="submit" class="btn btn-primary">Create</button>
            </div>
        </form>
    ```

## 43. Select list validation in ASP.NET Core MVC
```
    [Required]
    public Dept? Department { get; set; }
```

```
<div class="form-group row">
        <label asp-for="Department" class="col-sm-2 col-form-label"></label>
        <div class="col-sm-10">
            <select asp-for="Department" class="form-select col-sm-2"
                    asp-items="Html.GetEnumSelectList<Dept>()">
                <option value="">Please Select</option>
            </select>
            <span asp-validation-for="Department" class="text-danger"></span>
        </div>
    </div>
```

## 44. AddSingleton vs AddScoped vs AddTransient
- **With AddSingleton**, there is only a single instance. An instance is created, when the service is first requested and the single instance is used by all the http requests through out the application
- **With AddScoped**, we get the same instance withing the scope of a given http request but a new instance across different http requests
- **With AddTransient**, a new instance is provided every time an instance is requested whether it is in the scope of the same http request or across the different http requests
- Changes for testing
    - Program.cs
    ```
        builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
    ```
    - Controller
    ```
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = _employeeRepository.AddEmployee(employee);
                //return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }
    ```
    - View
    ```
        @inject IEmployeeRepository _empRepository
        ...
        <div class="form-group row">
            <div class="col-sm-10">
                Total no. of Employees: @_empRepository.GetEmployees().Count()
            </div>
        </div>
        ...
    ```

## 45. Introduction to Entity Framework Core
- ORM (Object Relational Mapper)
- Lightweight, Extensible and Open Source
- Works Cross Platform
- Microsoft's official Data access platform
- Two Approaches
    - Code First Approach
        - Domain and DBContext classes ==> EFCore ==> Database
    - Database First Approach
        - EFCore <== Database Provider ==> Database

## 46. Install Entity Framework Core in Visual Studio
- We have EF Core framework already installed in our Web Project.
- In different project, we can install EF Core with EF Core Provider
- EF Core Provider depends on EntityFrameworkCore.Relational and EntityFrameworkCore.Relational depends on EntityFrameworkCore. 
- Installing EF Core Provider will install dependencies as well

## 47. DbContext in Entity Framework Core
- To use DbContext class, we create a class which inherits from DbContext class provided by EntityFrameworkCore
- To pass configuration information to the DbContext use DbContextoptions instance
- The DbContext class incluldes DbSet<TEntity> property for each entity in the model
- We will use DbSet properties to query and save instances of DbSet class.
- The LINQ queries against the DbSet<TEntity> will be translated into SQL queries against the underlying Database

```
using Microsoft.EntityFrameworkCore;

namespace LearnAspNetCore.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {

    }

    public DbSet<Employee> Employees { get; set; }
}
```

## 48. Using SQL Server with Entity Framework Core
- AddDbContextPool vs AddDbContext
    - AddDbContextPool - Provides DbContext pooling which is better as performance standpoint
    - AddDbContext - Creates DbContext for each request
```
builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection")));

"ConnectionStrings": {
    "EmployeeDbConnection": "server=(localdb)\\MSSQLLocalDb;database=EmployeeDB;Trusted_Connection=true"
  }
```
- To use Intgrated Windows Authentication instead of SQL Server Authentication, we can use three settings:
    - Trusted_Connection=true
    - Integrated Security=true
    - Integrated Security=SSPI

## 49. Repository Pattern in ASP.NET Core
- In Repository pattern, our repository contains CRUD operations.
- We can have different Repository implementations (e.g. inmemory, sql etc.)
- With Dependecy Injection, Repository pattern is a strong tool as we can update the repository by simply updating repository we want to use.
- With DI and Repository Pattern, it makes our code decoupled and easily testable by mocking the Repositry.

## 50. Entity Framework Core Migrations
- Migration keeps the database schema and application model classed in sync
- Install EntityFramework tools => ``` Install-Package Microsoft.EntityFrameworkCore.Tools ```
- Get entity-framework core help => ``` Get-Help about_EntityFrameworkCore ```
- Add a new migration => ``` Add-Migration ```
- Update the database to a specified migration => ``` Update-Database ```

## 51. Entity Framework Core Seed Data
```
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
```

## 52. Keeping Domain Models and database schema in sync in ASP.NET Core
- Use **migrations** to keep domain models and database schema in sync
- To add a new migration use **Add-Migration** command
- To update database with latest migration use **Update-Database** command
- To remove the latest migration that is not yet applied to the database use **Remove-Migration**
- **__EFMigrationHistory** table is used to keep track of the migrations that are applied to the database
- **ModelSnapshot.cs** file contains the snapshot of the current model and is used to determine what has changed when adding the next migration
- To remove the migration the migration that is already applied to the database
    - First use the **Update-Database** command to undo the database changes applied by the migration
    - Next, use the **Remove-Migration** command to remove the migration code file

## 53. File upload in ASP.NET Core MVC
- EmployeeCreateViewModel
```
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name can't exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-93](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Official Email")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }

        public IFormFile Photo { get; set; }
    }
```

- HomeController
```
[HttpPost]
public IActionResult Create(EmployeeCreateViewModel model)
{
    if (ModelState.IsValid)
    {
        string uniqueFileName = null;
        if (model.Photo != null)
        {
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
        }

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
```

- Create.cshtml
```
<form enctype="multipart/form-data" asp-controller="home" asp-action="create" method="post" class="mt-3">
...
<div class="form-group row">
        <label asp-for="Photo" class="col-sm-2 col-form-label"></label>
        <div class="col-sm-10">
            <div class="custom-file">
                <input asp-for="Photo" class="form-control custom-file-input" />
                <label class="custom-file-label">Choose File...</label>
            </div>
        </div>
    </div>
    ...
```


## 54. Upload multiple files in ASP.NET Core MVC
- EmployeeCreateViewModel
```
 public List<IFormFile> Photos { get; set; }
```

- HomeController.cs
```
string uniqueFileName = null;
if (model.Photos != null && model.Photos.Count > 0)
{
    foreach (var photo in model.Photos)
    {
        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        photo.CopyTo(new FileStream(filePath, FileMode.Create));
    }
}
```

- Create.cshtml

```
<div class="form-group row">
        <label asp-for="Photos" class="col-sm-2 col-form-label"></label>
        <div class="col-sm-10">
            <div class="custom-file">
                <input multiple asp-for="Photos" class="form-control custom-file-input" />
                <label class="custom-file-label">Choose File...</label>
            </div>
        </div>
    </div>
```

## 55. Edit View in ASP.NET Core MVC
- Create Edit ViewModel class
```
public class EmployeeEditViewModel : EmployeeCreateViewModel
{
    public int Id { get; set; }

    public string ExistingPhotoPath { get; set; }
}
```
- Implement Edit Action that responds to HttpGet request
```
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
```
- Implement Edit View
```
@model EmployeeEditViewModel;

@{
    ViewBag.Title = "Edit Employee";
    var photoPath = "~/images/" + Model.ExistingPhotoPath ?? "noimage.jpg";
}

<form enctype="multipart/form-data" asp-controller="home" asp-action="edit" method="post" class="mt-3">
    <input hidden asp-asp-for="Id" />
    <input hidden asp-asp-for="ExistingPhotoPath" />
    .
    .
    .
    .

</form>
```

## 56. HttpPost Edit Action in ASP.NET Core MVC
```
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
```

## 57. Handling 404 Not Found in ASP.NET Core MVC
- Two types of 404 Errors:
    - Type 1: Resource with specified Id doesn't exist
    ```
        if(model == null)
        {
            Response.StatusCode = 404;
            return View("EmployeeNotFound", id.Value);
        }

        @model int;

        @{
            ViewBag.Title = "Employee Not Found";
        }

        <div class="alert alert-danger mt-1 mb-1">
            <h4>Not Found Error: </h4>
            <h5>
                Employee with Id: @Model cannot be found
            </h5>
        </div>
        <a asp-action="Index" asp-controller="Home" class="btn btn-success" style="width:auto">
            Click here to see list of all employees
        </a>
    ```
    - Type 2: The provided URL does not match any route

## 58. Centralised 404 Error Handling in ASP.NET Core MVC
- We have three middlewares for handling the Errors
    - ```app.UseStatusCodePages()``` => Shows plain text about the error
    - ```app.UseStatusCodePagesWithRedirects("/Error/{0}");``` => Renders provided View
    - ```app.UseStatusCodePagesWithReExecute("/Error/{0}");``` => Reders provided View
- Controller:
```
using Microsoft.AspNetCore.Mvc;

namespace LearnAspNetCore.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
            }
            return View("NotFound");
        }
    }
}

```
- View
```
@{
    ViewBag.Title = "Not Found";
}

<h1>
    @ViewBag.ErrorMessage
</h1>

<a asp-controller="home" asp-action="index">
    Click here to navigate to the home page
</a>
```

## 59. app.UseStatusCodePagesWithReExecute vs app.UseStatusCodePagesWithRedirects in ASP.NET Core MVC
- **UseStatusCodePagesWithRedirects**
    - Issues a redirect and the url in browser is changed to the redirect URL
    - Returns Success status(200) when actually an error  occurred which isn't semantically correct
- **UseStatusCodePagesWithReExecute**
    - Re-Executes the pipeline and returns the original status code (404 for example)
    - As it rexecutes the pipeline and not issue a redirect request, we also preserve the original URL in the browser
    - We can also access the path and query string with this middleware.
    ```
        var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                ViewBag.Path = statusCodeResult.OriginalPath;
                ViewBag.QS = statusCodeResult.OriginalQueryString;
                break;
        }

        return View("NotFound");
    ```

## 60. Global Exception handling in ASP.NET Core MVC
- Step 1: Add the Exception handling middleware to the request processing pipeline
```
app.UseExceptionHandler("/Error");
```
- Step2: Implement Error Controller
```
[Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
```
- Step 3: Implement custom error view
```
<h3> An Error occurred while processing your request.
    The support team is notified and we are working on this.
</h3>
<h5>
    Please contact us contact@LearnAspNetCore.com.
</h5>
<hr />
<h3>Exception Details:</h3>
<div class="alert alert-danger">
    <h5>Exception Path:</h5>
    <hr />
    <p>@ViewBag.ExceptionPath</p>
</div>
<div class="alert alert-danger">
    <h5>Exception Message:</h5>
    <hr />
    <p>@ViewBag.ExceptionMessage</p>
</div>
<div class="alert alert-danger">
    <h5>Exception StackTrace:</h5>
    <hr />
    <p>@ViewBag.StackTrace</p>
</div>
```

## 61. Logging in ASP.NET Core
- In ASP.NET Core, WebHost.CreateDefaultBuilder configures the logging
- We have logging section in appsettings where we can update the settings for logging
- ASP.NET Core Logging Provider
    - Logging providers stores or displays logs
        - **Console Logging Provider**: Displays logs on Console 
        - **Debug Logging Provider**: Displays logs on the debug window in Visual Studio
- Built-in Logging providers:
    - Console
    - Debug
    - EventSource
    - EventLog
    - TraceSource
    - AzureAppServicesFile
    - AzureAppServicesBlob
    - ApplicationInsights
- Third party logging providers
    - Nlog
    - elmah
    - Serilog
    - Sentry
    - Gelf
    - JSNLog
    - KissLog.net
    - Loggr

## 62. Logging exceptions in ASP.NET Core
- Inject an instance of ILogger interface
- Specify the type of the Controller into which ILogger is injected as the generic argument
```
public readonly ILogger<ErrorController> _logger;

public ErrorController(ILogger<ErrorController> logger)
{
    _logger = logger;
}
```
- Use the injected ILogger instance methods to log messages
    - LogInformation
    - LogWarning
    - LogError
    - etc...

## 63. Logging to file in ASP.NET core using nlog
- Install NLog.Web.AspNet.Core nuget package
- Create nlog.config
```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

	<!-- the targets to write to -->
	<targets>
		<!-- write logs to file  -->
		<target name="allfile" xsi:type="File"
				fileName="c:\DemoLogs\nlog-all-${shortdate}.log"/>
	</targets>

	<!-- rules to map from logger name to target -->
	<rules>
		<!--All logs, including from Microsoft-->
		<logger name="*" minlevel="Trace" writeTo="allfile" />
	</rules>
</nlog>
```
- Enable copy to bin folder
- Add NLog as one of the Logging Provider
```
builder.Logging.AddNLog();
```

## 64. ASP.NET Core LogLevel Configuration
- LogLevel indicates the severity of the logged messages
    - Trace = 0
    - Debug = 1
    - Information = 2
    - Warning = 3
    - Error = 4
    - Critical = 5
    - None = 6
    
    ```
        logger.LogTrace("Trace Log");
        logger.LogDebug("Debug Log");
        logger.LogInformation("Info Log");
        logger.LogWarning("Warning Log");
        logger.LogError("Error Log");
        logger.LogCritical("Critical Log");
    ```
- Logs can be filtered by:
    - Log Category
    - Logging Provider
    - Even Both
    ```
        "Logging": {
            "Debug": {
              "LogLevel": {
                "Default": "Warning",
                "LearnAspNetCore.Controllers.HomeController": "Warning",
                "LearnAspNetCore.Models.SQLEmployeeRepository": "Warning",
                "Microsoft.AspNetCore": "Warning"
              }
            },
            "LogLevel": {
              "Default": "Trace",
              "LearnAspNetCore.Controllers.HomeController": "Trace",
              "LearnAspNetCore.Models.SQLEmployeeRepository": "Trace",
              "Microsoft.AspNetCore": "Warning"
            }
          }
    ```

## 65. ASP.NET Core Identity Tutorial
- ASP.NET Core Identity is a Membershit system
    - Create, Read, Update, Delete user accounts
    - Account confirmation
    - Authentication and Authorization
    - Password Recovery
    - Two factor authentication with SMS
    - Supports external login providers like Microsoft, Google, Facebook etc.
    - and much more ....
- Steps:
    - Step 1: Application DbContext class must inherit from **IndentityDbContext**
    - Step 2: Add ASP.NET Core Identity Services
    ```
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
    ```
    - Step 3: Add Authentication middleware ``` app.UseAuthentication(); ```
    - Step 4: Generate ASP.NET Core Identity Tables
        - Call base.OnModelCreating in OnModelCreating() method in Application DbContext class
        ```
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Seed();
            }
        ```
        - Add-Migration
        - Update-Database

## 66. Registering new user using ASP.NET Core Identity
- Step 1: Add a new ViewModel - RegisterViewModel
- Step 2: Add new controller - RegisterController
- Step 3: Add a new Folder with name Account and add a new view with name Register in the same folder

## 67. ASP.NET Core Identity UserManager and SignIn Manager
```
using LearnAspNetCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnAspNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [Route("account/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("account/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}

```

## 68. ASP.NET Core Identity Password complexity
- Two ways:
    - Configure with AddIdentity
    ```
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
            options.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<AppDbContext>();
    ```
    - Configure Separately
    ```
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
            options.Password.RequireNonAlphanumeric = false;
        });
    ```

## 69. Show or hide login and logout links based on login status
- View
```
<ul class="navbar-nav ml-auto">
    @if (signInManager.IsSignedIn(User))
    {
        <li class="nav-item">
            <form method="post" asp-action="logout" asp-controller="account">
                <button type="submit" class="nav-link btn btn-link py-0" style="width:auto">
                    Logout @User.Identity.Name  
                </button>
            </form>
        </li>
    }
    else
    {
        <li class="nav-item">
            <a asp-controller="account" asp-action="register" class="nav-link">Register</a>
        </li>
        <li class="nav-item">
            <a asp-controller="account" asp-action="login" class="nav-link">Login</a>
        </li>
    }
</ul>
```
- Controller
```
[HttpPost]
public async Task<IActionResult> Logout()
{
    await signInManager.SignOutAsync();
    return RedirectToAction("index", "home");

}
```

## 70. Implementing Login Functionality in ASP.NET Core
- LoginViewModel
```
using System.ComponentModel.DataAnnotations;

namespace LearnAspNetCore.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
```

- View
```
@model LoginViewModel;

@{
    ViewBag.Title = "User Login";
}

<h1>User Login</h1>

<div class="row">
    <div class="col-md-12">
        <form method="post">
            <div asp-validation-summary="All" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="Email"></label>
                <input asp-for="Email" class="form-control" />
                <span asp-validation-for="Email" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Password"></label>
                <input asp-for="Password" class="form-control" />
                <span asp-validation-for="Password" class="text-danger"></span>
            </div>
            <div class="form-group">
                <div class="checkbox">
                    <label asp-for="RememberMe">
                        <input asp-for="RememberMe" />
                        @Html.DisplayNameFor(m => m.RememberMe)
                    </label>
                </div>
            </div>
            <button type="submit" class="btn btn-primary">Login</button>
        </form>
    </div>
</div>
```

- Controller Action Methods
```
[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            return View(model);
        }
```

- Persistent Cookie vs Session Cookie
    - Session Cookie is removed on closing the browser.
    - Persistent cookies stay there even on closing the browser. To remove the cookie, we should logout.

## 71. Authorization in ASP.NET Core
- Authentication vs Authorization
    - Authentication is identifying who the user is
    - Authorization is identifying what the can or cannot do.
- Apply Authorize Attribute on Action level
    ```
        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }
    ```
- Apply Authorize Attribute at Controller level
    ```
        [Authorize]
        public class HomeController : Controller
        {
    ```
- Apply Authorize Attribute globally
    ```
        builder.Services.AddMvc(options =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            options.Filters.Add(new AuthorizeFilter(policy));
            options.EnableEndpointRouting = false;
        })
    ```
- Apply AllowAnonymous Attribute to allow access to allow access to all.
    ```
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetEmployees();
            return View(model);
        }
    ```
- What happen if AllowAnonymous attribute is applied at controller level and Authorize attribute at Action level in that controller?
    - Authorize attribute will be ignored
- Types of Authorization
    - Role based Authorization
    - Claims based Authorization
    - Policy based Authorization
    
## 72. Redirect user to original URL after login in ASP.NET Core
```
 [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("index", "home");
                else
                    return Redirect(returnUrl);
            }

            ModelState.AddModelError("", "Invalid Login Attempt");
        }

        return View(model);
    }
```

## 73. Open Redirect Vulnerability Example
- Application Vulnerable to Open Redirect Vulnerability Attacks
    - Your application redirects to a URL that's specified via the request such as querystring or form data
    - The redirection is performed without checking if the URL is a local URL
- Example
    - The user is tricked into clicking a link in an email where the email returnUrl is set to attackers website.
    https://example.com/account.login?returnUrl=http://exampple.com/account.login
    - The user logs in successfully on the authentic website and he is redirected to the attacker website
    - The user logs in again on the attacker website, thinking that the first login attempt was not successful
    - The user is then redirected back to the authentic website
    - During this entire process, the login credentials for the authentic website are compromised
- Use LocalRedirect()
```
if (result.Succeeded)
{
    if (string.IsNullOrEmpty(returnUrl))
        return RedirectToAction("index", "home");
    else
        return LocalRedirect(returnUrl);
}
```
- Use Url.IsLocalUrl()
```
if (result.Succeeded)
{
    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        return Redirect(returnUrl);
    else
        return RedirectToAction("index", "home");
}
```

## 74. ASP.NET core client side validation
- Add Following jquery libraries in order
    ```
        <script src="~/lib/jquery/jquery.js"></script>
        <script src="~/lib/jquery-validate/jquery.validate.js"></script>
        <script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"></script>
    ```
- Validation attributes added in the server side will be handled on UI unobtrusive.js
- jquery.validate.unobtrusive.js reads data-val attributes in UI and perform the validation
```
<input class="form-control" type="email" data-val="true" data-val-email="The Email field is not a valid e-mail address." data-val-required="The Email field is required." id="Email" name="Email" value="">
```
- Make sure the order is correct
- Make sure client side javascript is not disabled

## 75. ASP.NET Core remote validation
- 