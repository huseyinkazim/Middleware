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
//Use kullan�m� biz next(); �a��rarak bir sonraki middleware e iletiyoruz
app.Use(async (context, next) =>
{
	await context.Response.WriteAsync("Use Middleware1 Incoming Request \n");
	await next();
	await context.Response.WriteAsync("Use Middleware1 Outgoing Response \n");
});
//Use kullan�m� biz next(); �a��rarak bir sonraki middleware e iletiyoruz
app.Use(async (context, next) =>
{
	await context.Response.WriteAsync("Use Middleware2 Incoming Request \n");
	await next();
	await context.Response.WriteAsync("Use Middleware2 Outgoing Response \n");
});
//Run kullanarak son i�lem oldu�unu belirtiyoruz burda i�lemimizi yap�p bizden �nce next(); operasyon �a�r�m�nda bulunan
//Middlewareler next sonraki i�lemlerini yaparak i�lemler biter
app.Run(async context => {
	await context.Response.WriteAsync("Run Middleware3 Request Handled and Response Generated\n");
});

//burdaki Run operasyonuna Pipeline gelemez ��nk� ondan �nceki run operasyonu pipe � bitirdi
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
