namespace WebAPI.Middlewares
{
	public class LoggerMiddleware : IMiddleware
	{
		private readonly ILogger<LoggerMiddleware> _logger;

		public LoggerMiddleware(ILogger<LoggerMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			_logger.LogInformation("request geldi");

			//request ilk geldiğinde işlem yapılacak alan
			await context.Response.WriteAsync("SecurityMiddleware control Incoming Request \n");

			await next.Invoke(context);//Bir sonraki işlemin yapılacağı yere gönderilen yer

			//response dönülürken işlem yapılacak alan
			await context.Response.WriteAsync("SecurityMiddleware control Outgoing Response \n");
			_logger.LogInformation("response gitti");

		}
	}
}
