using CoreLayer.models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
	public interface IOrderService
	{
		//--Create Inerface IOrderService[CreateOrderAsync,GetOrdersForUserAsync,GetOrderByIdForUser,GetDeliveryMethodAsync]
		public Task<Order?> CreateOrderAsync(string ClientEmail,ShippingAddress shippingAddress, string basketId);
		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string ClientEmail);
		public Task<Order?> GetOrderByIdForUser(string ClientEmail, int orderId);
		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();


	}
}
