using CoreLayer.Interfaces;
using CoreLayer.models.BasketModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Talabat_APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		private readonly ILogger<PaymentController> _logger;
		private readonly IConfiguration _configuration;

		public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger,IConfiguration configuration)
		{
			_paymentService = paymentService;
			_logger = logger;
			_configuration = configuration;
		}
		[HttpGet]
		public async Task<ActionResult<Basket>> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basket == null) { return BadRequest("An Error Occured While Creating PaymentIntent"); }
			return Ok(basket);
		}

		[HttpPost("webhook")]
		public async Task<IActionResult> Webhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			var stripeEvent = EventUtility.ConstructEvent(
				json,
				Request.Headers["Stripe-Signature"],
				_configuration["Payment:WebHookSecretKey"]
			);

			if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
			{
				var paymentIntentId = ((PaymentIntent)stripeEvent.Data.Object).Id;
				await _paymentService.OrderPaymentStatusAsync(paymentIntentId, true);
				_logger.LogInformation("✅ PaymentIntent was successful!");
			}
			else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
			{
				var paymentIntentId = ((PaymentIntent)stripeEvent.Data.Object).Id;
				await _paymentService.OrderPaymentStatusAsync(paymentIntentId, false);
				_logger.LogInformation("❌ PaymentIntent was failed!");
			}

			return Ok();
		}
	}
}
