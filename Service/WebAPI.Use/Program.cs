var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();
var summaries = new[]
{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
	var forecast = Enumerable.Range(1, 5).Select(index =>
		new WeatherForecast
		(
			DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			Random.Shared.Next(-20, 55),
			summaries[Random.Shared.Next(summaries.Length)]
		))
		.ToArray();
	return forecast;
});
//Use kullanımı biz next(); çağırarak bir sonraki middleware e iletiyoruz
app.Use(async (context, next) =>
{
	await context.Response.WriteAsync("Use Middleware1 Incoming Request \n");
	await next();
	await context.Response.WriteAsync("Use Middleware1 Outgoing Response \n");
});
//Use kullanımı biz next(); çağırarak bir sonraki middleware e iletiyoruz
app.Use(async (context, next) =>
{
	await context.Response.WriteAsync("Use Middleware2 Incoming Request \n");
	await next();
	await context.Response.WriteAsync("Use Middleware2 Outgoing Response \n");
});
//Run kullanarak son işlem olduğunu belirtiyoruz burda işlemimizi yapıp bizden önce next(); operasyon çağrımında bulunan
//Middlewareler next sonraki işlemlerini yaparak işlemler biter
app.Run(async context => {
	await context.Response.WriteAsync("Run Middleware3 Request Handled and Response Generated\n");
});

//burdaki Run operasyonuna Pipeline gelemez çünkü ondan önceki run operasyonu pipe ı bitirdi
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
