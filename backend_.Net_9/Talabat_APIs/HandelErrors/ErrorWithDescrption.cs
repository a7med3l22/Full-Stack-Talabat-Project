namespace Talabat_APIs.HandelErrors
{
	public class ErrorWithDescrption:ApiErrors
	{
		public string? Description { get; set; }
		public ErrorWithDescrption(int code,string?message=null,string?description=null):base(code, message)
		{
			Description = description;
		}
	}
}
