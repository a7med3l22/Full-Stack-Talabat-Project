using Microsoft.EntityFrameworkCore;

namespace CoreLayer.models.Order
{
	[Owned]
	public class ShippingAddress
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Street { get; set; } = null!;
		public string City { get; set; } = null!;
		public string Country { get; set; } = null!;
	}
}