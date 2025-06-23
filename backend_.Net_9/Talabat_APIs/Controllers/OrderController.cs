using AutoMapper;
using CoreLayer.Interfaces;
using CoreLayer.models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Talabat_APIs.Dto.OrderDto;
using Talabat_APIs.HandelErrors;

namespace Talabat_APIs.Controllers
{
	[Authorize]
	public class OrderController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService,IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}
		//--Create Ordercontroller[create order,Get Orders For Spectfic User By His Email,Get Spectfic Order By Order Id For Spectfic User By His Email,GetDelivery Methods]
		[HttpPost]
		public async Task<ActionResult<orderToReturnDto>> CreateOrderAsync(OrderParamsDto orderParams)
		{
			var TokenEmail = GetTokenEmail().Value!;
			var order = await _orderService.CreateOrderAsync(TokenEmail, orderParams.shippingAddress, orderParams.basketId);
			if (order == null) { return BadRequest(new ApiErrors(400, "ann error occured during create order!")); }
			var returnedOrder= _mapper.Map<orderToReturnDto>(order);
			return Ok(returnedOrder);
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<orderToReturnDto>>> GetOrdersForUserAsync()
		{
			var TokenEmail = GetTokenEmail().Value!;
			var orders = await _orderService.GetOrdersForUserAsync(TokenEmail);
			if (orders.Count <= 0) { return NotFound(new ApiErrors(404, "not found any orders for this email!")); }
			var returnedOrders = _mapper.Map<IReadOnlyList<orderToReturnDto>>(orders);
			return Ok(returnedOrders);
			
		}
		[HttpGet("{orderId}")]
		public async Task<ActionResult<IReadOnlyList<orderToReturnDto>>> GetOrdersForUserAsync(int orderId)
		{
			var TokenEmail = GetTokenEmail().Value!;
			var order = await _orderService.GetOrderByIdForUser(TokenEmail, orderId);
			if (order == null) { return NotFound(new ApiErrors(404, "not found any orders for this email!")); }
			var returnedOrder = _mapper.Map<orderToReturnDto>(order);
			return Ok(returnedOrder);
		
		}

		[HttpGet("DeliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
		{
			var DeliveryMethods = await _orderService.GetDeliveryMethodsAsync();
			if (DeliveryMethods.Count <= 0) { return NotFound(new ApiErrors(404, "not found any DeliveryMethod")); }
			return Ok(DeliveryMethods);
		}

		private ActionResult<string> GetTokenEmail()
		{
			var TokenEmail = User.FindFirstValue(ClaimTypes.Email);
			if (TokenEmail == null) { return BadRequest(new ApiErrors(400, "invalid token")); }
			return TokenEmail;
		}
	}
}
