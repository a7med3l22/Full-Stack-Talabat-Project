
namespace Talabat_APIs.HandelErrors
{
	public  class ApiErrors
	{
		public int Code { get; set; }
		public string ? Message { get; set; }

		public ApiErrors(int code,string?message=null)
		{
			Code = code;
			Message = message??DefaultErrorMessage(code);
		}

		private string? DefaultErrorMessage(int code)
		{
			return code switch
			{
				200 => "OK",
				201 => "Created",
				204 => "No Content",

				400 => "Bad Request",
				401 => "Unauthorized",
				403 => "Forbidden",
				404 => "Not Found",
				405 => "Method Not Allowed",
				409 => "Conflict",
				422 => "Unprocessable Entity",

				500 => "Internal Server Error",
				502 => "Bad Gateway",
				503 => "Service Unavailable",
				504 => "Gateway Timeout",

				_ => "Unknown Status Code"
			};
		}
	}
}
