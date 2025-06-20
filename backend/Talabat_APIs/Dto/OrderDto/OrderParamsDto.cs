using CoreLayer.models.Order;

namespace Talabat_APIs.Dto.OrderDto
{
	public class OrderParamsDto
	{
		public ShippingAddress shippingAddress { get; set; } = null!;
		//public int deliveryMethodId { get; set; }
		public string basketId { get; set; } = null!;
	}
}
