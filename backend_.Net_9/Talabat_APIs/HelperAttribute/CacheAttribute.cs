using CoreLayer.Interfaces;
using CoreLayer.models.CacheModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Talabat_APIs.HelperAttribute
{
	public class CacheAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int? _timeToLifeInMinutes;

		public CacheAttribute(int timeToLifeInMinutes)
		{
			_timeToLifeInMinutes = timeToLifeInMinutes;
		}
		public CacheAttribute()
		{
			
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			//generate Cashe Key based on the request
			var cacheKey = GetCacheKey(context);
			//Check if the cache key exists in the Redis cache
			var cacheResponse = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();   //get cache from DI
			var CacheResponse = await cacheResponse.GetCacheAsync(cacheKey);
			if (CacheResponse != null)
			{
				context.Result = new ContentResult
				{
					Content = CacheResponse.Data,
					StatusCode = CacheResponse.StatusCode,
					ContentType = "application/json"
				};
				return; // هيخرج مش هينفذ ال action
			}
			var excutedAction =await next.Invoke();
			if(excutedAction.Result is ObjectResult objectResult && objectResult.Value is not null)
			{
				var valueString= objectResult.Value is string str ?str:JsonSerializer.Serialize(objectResult.Value, cacheResponse.JsonOptions);//لكن لو القيمة مش string (زي object, List, ProductDto, إلخ...)، لازم نعمل لها Serialize عشان تتحول لـ JSON ونقدر نخزنها في الكاش.
				var cacheResponseProperties =new CacheResponseProperties() { Data= valueString, StatusCode=objectResult.StatusCode??StatusCodes.Status200OK };
				var timeSpan = _timeToLifeInMinutes == null ? TimeSpan.FromMinutes(5) : TimeSpan.FromMinutes(_timeToLifeInMinutes.Value); // Default Is 5 Minutes
				await cacheResponse.CreateCacheAsync(cacheKey, cacheResponseProperties, timeSpan);
			}

		}

		private static string GetCacheKey(ActionExecutingContext context)
		{
			// Get/api/product?brandID=2&PagESize=3&PageIndex=2
			//Get Method
			var method = context.HttpContext.Request.Method;//Get
			//Get Path
			var path = context.HttpContext.Request.Path.Value?.Trim('/')??string.Empty; //api/product/
			//Get Query 
			var query = context.HttpContext.Request.Query; //brandID=2&PagESize=3&PageIndex=2
			
			if (query.Count == 0)
				return $"{method}:{path}".ToUpperInvariant();

			var stringQuery = query
				.OrderBy(q => q.Key) // لضمان ترتيب ثابت
				.Select(q =>
				{
					var key = q.Key;
					//Email=[]
					//Email=[ahmed@alaa,alaa@kamal,null]
					var values = q.Value
						.Where(v => !string.IsNullOrWhiteSpace(v)) //[ahmed@alaa,alaa@kamal]
						.Select(v => Uri.EscapeDataString(v!))//["ahmed%40alaa", "alaa%40kamal"]
						.ToList();

					return values.Count > 0
						? $"{key}={string.Join(",", values)}" //EMAIL=ahmed%40alaa,alaa%40kamal
						: key; //Email
				});

			return $"{method}:{path}?{string.Join("&", stringQuery)}".ToUpperInvariant(); 
		}

	}
}

