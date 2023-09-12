namespace WebAPI.Middlewares
{
	public class SecurityMiddleware
	{
		private readonly RequestDelegate _next;

		public SecurityMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			//request ilk geldiğinde işlem yapılacak alan
			await context.Response.WriteAsync("SecurityMiddleware control Incoming Request \n");

			await _next.Invoke(context);//Bir sonraki işlemin yapılacağı yere gönderilen yer

			//response dönülürken işlem yapılacak alan
			await context.Response.WriteAsync("SecurityMiddleware control Outgoing Response \n");
		}


	}
}
