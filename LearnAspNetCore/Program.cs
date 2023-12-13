var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => System.Diagnostics.Process.GetCurrentProcess().ProcessName);

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello from First");
    await next();
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from Second");
});
