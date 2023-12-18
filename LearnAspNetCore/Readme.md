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
