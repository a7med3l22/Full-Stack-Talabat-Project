using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.models.Order
{
	public class Order:BaseClass
	{
		//--Create class Order : BaseClass[ClientEmail - OrderDate - OrderState - ShippingAddress - deliveryMethod - OrderItems - SubTotal - GetTotal - PaymentIntentId]
		//--Create enum OrderState[Pending-PaymentReceived-PaymentFailed]
		//--Create class ShippingAddress[FirstName-LastName-Street-City-Country]
		//--Create class DeliveryMethod : BaseClass[ShortName - Description - Cost - DeliveryTime]
		//--Create class OrderItem : BaseClass[ProductItemOrdered - Price - Quantity] 
		//--Create class ProductItemOrdered Contain[ProductId, ProductName, PictureUrl
		public string ClientEmail { get; set; } = null!;
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
		public OrderState orderState { get; set; } = OrderState.Pending;
		public ShippingAddress shippingAddress { get; set; } = null!;
		public DeliveryMethod? deliveryMethod { get; set; }
		public ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();
		public decimal SubTotal { get; set; }


		[NotMapped]
		public decimal Total => SubTotal + deliveryMethod?.Cost ?? 0;
		//OR
		//public decimal GetTotal()=> SubTotal + deliveryMethod?.Cost ?? 0;

		public string PaymentIntentId { get; set; } = string.Empty; // علشان لو مبعتش قيمة ميضربش اكسيبشن

	}
}
