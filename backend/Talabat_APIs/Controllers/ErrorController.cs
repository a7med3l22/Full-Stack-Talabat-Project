using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_APIs.HandelErrors;

namespace Talabat_APIs.Controllers
{
	[Route("error")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]

	public class ErrorController : ControllerBase
	{
		[HttpGet("{Errorcode}")]
		public ActionResult handelError(int Errorcode)
		{
			return new ObjectResult(new ApiErrors(Errorcode));

		}
	}
}
