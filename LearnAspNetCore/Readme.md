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

