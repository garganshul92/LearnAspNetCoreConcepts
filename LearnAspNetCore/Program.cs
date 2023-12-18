using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => System.Diagnostics.Process.GetCurrentProcess().ProcessName);

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello from First");
    await next();
});

/*
//DefaultFilesOptions options = new DefaultFilesOptions();
//options.DefaultFileNames.Clear();
//options.DefaultFileNames.Add("staticHtmlFile.html");
//app.UseDefaultFiles(options);
//app.UseStaticFiles();
*/

FileServerOptions options = new FileServerOptions();
options.DefaultFilesOptions.DefaultFileNames.Clear();
options.DefaultFilesOptions.DefaultFileNames.Add("staticHtmlFile.html");
app.UseFileServer();

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from Second");
});
