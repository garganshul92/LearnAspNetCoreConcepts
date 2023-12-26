var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    DeveloperExceptionPageOptions options = new();
    options.SourceCodeLineCount = 10;
    app.UseDeveloperExceptionPage(options);
}

app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
app.Run();