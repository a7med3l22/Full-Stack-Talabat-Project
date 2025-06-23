using CoreLayer.Generic_Specification.Order_Specefication;
using CoreLayer.Interfaces;
using CoreLayer.models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Generic_Specification.OrderSpecefication
{
	public class OrderSpecifications:BaseSpecefication<Order>
	{
		public OrderSpecifications(SpecParams orderParams):
			base(o=>
			(string.IsNullOrEmpty(orderParams.Email)|| o.ClientEmail== orderParams.Email) &&
			(string.IsNullOrEmpty(orderParams.PaymentIntentId) || o.PaymentIntentId == orderParams.PaymentIntentId) &&
			(!orderParams.orderId.HasValue||o.Id== orderParams.orderId))
		{
			if (orderParams.orderId.HasValue) { SetIncludes(); }
			if (!string.IsNullOrEmpty(orderParams.Email)&& !orderParams.orderId.HasValue) { SetIncludes(); OrderByDesc = o => o.OrderDate; }
		}
		private void SetIncludes()
		{
			Includes.Add(o => o.deliveryMethod!);
			Includes.Add(o => o.orderItems);
		}

	}
}
