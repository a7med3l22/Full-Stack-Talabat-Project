namespace Talabat_APIs.HandelErrors
{
	public class validationError:ApiErrors // دي لو انا بعتت للكنترولر type غلط 
	{
		public List<string> Errors { get; set; }
		public validationError(List<string> errors) :base(400)
		{
			Errors=errors;
		}
	}
}
