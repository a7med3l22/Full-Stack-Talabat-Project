using CoreLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class ApiUrl : IApiUrl
	{
		private readonly IHttpContextAccessor _httpContext;

		public ApiUrl(IHttpContextAccessor httpContext)
		{
			_httpContext = httpContext;
		}

		public string GetApiUrl()
		{
			var context = _httpContext.HttpContext; //You Must Use It In Scoped or Transient Lifetime, Not Singleton Or It Will Be Null And Will Throw Exception.
			if (context == null)
				throw new InvalidOperationException("HttpContext is not available outside the request scope."); 

			var scheme = context.Request.Scheme;
			var host = context.Request.Host.Value;

			return $"{scheme}://{host}/";
		}
	}
}
