using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<LoggerMiddleware>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<SecurityMiddleware>();
app.UseMiddleware<LoggerMiddleware>();



app.UseAuthorization();

app.MapControllers();

app.Run();
