using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.models.BasketModels
{
	public class Basket
	{
		public string Id { get; set; } = null!;
		public string? PaymentIntentId { get; set; }
		public decimal ShippingCost { get; set; }
		public string? ClientSecret { get; set; }
		public int? DeliveryMethodId { get; set; }
		public List<BasketItems> Items { get; set; } = new();
		public Basket(string id)
		{
			Id = id;
		}
	}
}
