using CoreLayer.Generic_Specification.Order_Specefication;
using CoreLayer.Generic_Specification.OrderSpecefication;
using CoreLayer.Interfaces;
using CoreLayer.models.BasketModels;
using CoreLayer.models.Order;
using CoreLayer.models.ProductModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.BasketRepository;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product= CoreLayer.models.ProductModels.Product; // Assuming Product is defined in CoreLayer.models.ProductModels namespace
namespace ServiceLayer.Entity_Service
{
	public class PaymentService:IPaymentService
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IConfiguration _config;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService(IBasketRepository basketRepo,IConfiguration config,IUnitOfWork unitOfWork)
		{
			_basketRepo = basketRepo;
			_config = config;
			_unitOfWork = unitOfWork;
		}
		public async Task<Basket?> CreateOrUpdatePaymentIntent(string basketId)
		{
			//create a payment intent 
			StripeConfiguration.ApiKey=_config["Payment:SecretKey"];

			var basket = await _basketRepo.GetAsync(basketId);
			if(basket == null||basket.Items.Count==0||basket.DeliveryMethodId==null) { return null; }
			//calculate amount of items in the basket
			foreach (var item in basket.Items)
			{
				var product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id); //get product by id from the basket item
				if (product == null) { return null; }
				if(item.Price!= product.Price) //if price is not equal to the product price, update the item price
				{
					item.Price = product.Price;
				}
			}
			var basketItemsAmount = basket.Items.Sum(item => item.Price * item.Quantity);
			if(basketItemsAmount <= 0) { return null; } //if total amount is less than or equal to 0, return null

			//get amount of deliveryMethod
			var deliveryMethodRepo = _unitOfWork.Repository<DeliveryMethod>();
			var deliveryMethod = await deliveryMethodRepo.GetByIdAsync(basket.DeliveryMethodId.Value);
			if(deliveryMethod == null) { return null; } //if delivery method is not found, return null
			//create or update payment intent
			var amount = (long)(basketItemsAmount + deliveryMethod.Cost)*100; //total amount of items in the basket + delivery method price
			var service = new PaymentIntentService();
			if(string.IsNullOrEmpty(basket.PaymentIntentId)) //create paymentIntent
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = amount,
					Currency = "usd",
					AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
					{
						Enabled = true,
					},
				};
				var paymentIntent= await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id; //set payment intent id to the basket
				basket.ClientSecret = paymentIntent.ClientSecret; //set client secret to the basket
				var updatedBasket = await _basketRepo.UpdateOrAddAsync(basket); //update basket with payment intent id
				if (updatedBasket == null) { return null; } //if basket is not updated, return null


			}
			else //update paymentIntent
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = amount
				};
				await service.UpdateAsync(basket.PaymentIntentId, options);
			}
			return basket; //return client secret
		}
		public async Task OrderPaymentStatusAsync(string PaymentIntentId, bool IsSucceeded)
		{
			// i access it after payment intent is succeeded or failed after Creating Order
			var orderRepo = _unitOfWork.Repository<Order>();
			var specParams=new SpecParams
			{
				PaymentIntentId = PaymentIntentId
			};
			var orderSpec = new OrderSpecifications(specParams);
			var order = (await orderRepo.GetFirstOrDefaultSpecAsync(orderSpec))!; // i sure that orders have only one order with this spec
			if(IsSucceeded)
			{
				order.orderState = OrderState.PaymentReceived; //set order state to payment received
			}
			else
			{
				order.orderState = OrderState.PaymentFailed; //set order state to payment received
			}
			//update order state in db
			orderRepo.update(order);      //test it what happen in deliveryMethodId is Will Be Null Or Not Changed
			await _unitOfWork.CompleteAsync(); //save changes to db 
		}
	}
}
