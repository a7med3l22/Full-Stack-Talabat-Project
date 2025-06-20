namespace Talabat_APIs.Dto.OrderDto
{
	public class DeliveryMethodDto
	{
		public int Id { get; set; }
		public string ShortName { get; set; } = null!;
		public decimal Cost { get; set; }
	}
}
