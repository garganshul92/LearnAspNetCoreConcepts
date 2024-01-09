using LearnAspNetCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddNLog();

builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

var app = builder.Build();

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    DeveloperExceptionPageOptions options = new();
    options.SourceCodeLineCount = 10;
    app.UseDeveloperExceptionPage(options);
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseMvc(routes =>
{
    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
});
app.Run();