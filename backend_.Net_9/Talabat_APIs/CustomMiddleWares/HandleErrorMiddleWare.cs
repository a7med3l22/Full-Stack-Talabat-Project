
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Talabat_APIs.HandelErrors;

namespace Talabat_APIs.CustomMiddleWares
{
	public class HandleErrorMiddleWare : IMiddleware
	{
		private readonly ILogger _log;
		private readonly IHostEnvironment _env;

		//enviroment,RequestDelegate,ilogger
		public HandleErrorMiddleWare(ILogger<HandleErrorMiddleWare> log,IHostEnvironment env)
		{
			_log = log;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{


			try
			{
				await next(context);   //هنا بيوديك لل ميدلوير اللي بعدها وشايله ال ريكويست
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				if (!context.Response.HasStarted)
				{
					context.Response.ContentType = "application/json";
					context.Response.StatusCode = StatusCodes.Status500InternalServerError;


					var response = _env.IsDevelopment() ? new ErrorWithDescrption(context.Response.StatusCode, ex.Message, ex.StackTrace) : new ErrorWithDescrption(context.Response.StatusCode);
					var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
					var json = JsonSerializer.Serialize(response, option);
					await context.Response.WriteAsync(json);
				}
			}

		}
	}
}
