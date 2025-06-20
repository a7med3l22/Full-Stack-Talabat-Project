namespace CoreLayer.models.Order
{
	public class DeliveryMethod:BaseClass
	{
		// BaseClass[ShortName - Description - Cost - DeliveryTime]
		public string ShortName { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Cost { get; set; }
		public string DeliveryTime { get; set; } = null!;
	}
}