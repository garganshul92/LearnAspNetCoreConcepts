## 4. ASP NET core Project Files
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