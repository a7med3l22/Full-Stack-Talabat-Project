using CoreLayer.models.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat_APIs.Dto.OrderDto
{
	public class orderToReturnDto
	{
		public int Id { get; set; }
		public string ClientEmail { get; set; } = null!;
		public DateTimeOffset OrderDate { get; set; } 
		public string orderState { get; set; } = null!;
		public ShippingAddress shippingAddress { get; set; } = null!;
		public decimal deliveryMethodCost { get; set; }
		public ICollection<OrderItemDto> Items { get; set; } = null!;
		public decimal SubTotal { get; set; }
		public decimal Total { get; set; }
		//public string PaymentIntentId { get; set; } = string.Empty; 

	}
}
