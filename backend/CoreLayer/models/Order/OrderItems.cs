namespace CoreLayer.models.Order
{
	public class OrderItem:BaseClass
	{
		//--Create class OrderItem : BaseClass[ProductItemOrdered - Price - Quantity] 
		public ProductItemOrdered productItemOrdered { get; set; } = null!;
		public decimal Price { get; set; }
		public int Quantity { get; set; }

	}
}