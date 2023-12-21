var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => System.Diagnostics.Process.GetCurrentProcess().ProcessName);

if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    DeveloperExceptionPageOptions options = new();
    options.SourceCodeLineCount = 10;
    app.UseDeveloperExceptionPage(options);
}

app.UseFileServer();

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from Second");
});
