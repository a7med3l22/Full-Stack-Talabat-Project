using CoreLayer.models.BasketModels;

namespace Talabat_APIs.Dto.Basket
{
	public class BasketDto
	{
		public string Id { get; set; } = null!;
		public List<BasketItemsDto> Items { get; set; } = new();
		public int? DeliveryMethodId { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? ClientSecret { get; set; }
		public decimal ShippingCost { get; set; }
	}
}
