using CoreLayer.Generic_Specification.Order_Specefication;
using CoreLayer.Generic_Specification.OrderSpecefication;
using CoreLayer.Interfaces;
using CoreLayer.models.Order;
using CoreLayer.models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using RepositoryLayer.BasketRepository;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = CoreLayer.models.ProductModels.Product; // Assuming Product is defined in CoreLayer.models.ProductModels namespace

namespace ServiceLayer.Order_Service
{
	[Authorize]
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBasketRepository _basketRepo;
		private readonly IPaymentService _paymentService;

		public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo,IPaymentService paymentService)
		{
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
			_paymentService = paymentService;
		}
		public async Task<Order?> CreateOrderAsync(string ClientEmail, ShippingAddress shippingAddress, string basketId)
		{
			var basket =await _basketRepo.GetAsync(basketId);
			if (basket == null || basket.Items.Count == 0 || basket.DeliveryMethodId == null|| string.IsNullOrEmpty(basket.PaymentIntentId) ) { return null; } //To Create Order must have basket and basket must have orderitems and also have PaymentIntentId and also have DeliveryMethodId
			var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
			//<<check if there is any order that have same PaymentIntentId or not>>
			var specParams = new SpecParams
			{
				PaymentIntentId = basket.PaymentIntentId
			};
			var orderSpec = new OrderSpecifications(specParams);
			var ordersRepo = _unitOfWork.Repository<Order>();
			var orders=await ordersRepo.GetAllSpecAsync(orderSpec);
			if(orders.Count!=0)
			{
				foreach(var orderItem in orders)
				{
					ordersRepo.remove(orderItem); 
				}
				//update payment intent Amount 
				await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			}
			//i want to make list of items that be in basket
			var orderItems = new List<OrderItem>();
				var productReop = _unitOfWork.Repository<Product>();
				foreach (var item in basket.Items)
				{
					var product =await productReop.GetByIdAsync(item.Id);//item id in basketitems is equal to product id
					if (product is null) { return null; }
				
						var ProductItemOrdered = new ProductItemOrdered()
						{
							ProductId = product.Id,
							PictureUrl = product.PictureUrl,
							ProductName = product.Name,
						};
						var orderitem = new OrderItem()
						{
							Price = product.Price,
							productItemOrdered = ProductItemOrdered,
							Quantity = item.Quantity
						};
						orderItems.Add(orderitem);
					
				}
					var SubTotal = orderItems.Sum(OI => OI.Price*OI.Quantity);
					var order = new Order()
					{
						ClientEmail = ClientEmail,
						shippingAddress = shippingAddress,
						deliveryMethod = DeliveryMethod,
						orderItems = orderItems,
						SubTotal = SubTotal,
						PaymentIntentId=basket.PaymentIntentId
					};
					//add order to db
					 _unitOfWork.Repository<Order>().insert(order);
					//save to Db
					var result=await _unitOfWork.CompleteAsync();
					if (result <= 0) { return null; }
					return order;
		}
		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string ClientEmail)
		{
			var specParams= new SpecParams()
			{
				Email = ClientEmail
			};
			var specOrders=new OrderSpecifications(specParams);
			var orders= await _unitOfWork.Repository<Order>().GetAllSpecAsync(specOrders);
			return orders;
		}
		public async Task<Order?> GetOrderByIdForUser(string ClientEmail, int orderId)
		{
			var specParams= new SpecParams()
			{
				Email = ClientEmail,
				orderId = orderId
			};
			var specOrder= new OrderSpecifications(specParams);
			var order =await _unitOfWork.Repository<Order>().GetFirstOrDefaultSpecAsync(specOrder);
			return order;
		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync() =>
			await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
		

		
	
	}
}
