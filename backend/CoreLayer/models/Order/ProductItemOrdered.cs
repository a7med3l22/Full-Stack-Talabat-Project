using Microsoft.EntityFrameworkCore;

namespace CoreLayer.models.Order
{
	[Owned]
	public class ProductItemOrdered
	{
		//--Create class ProductItemOrdered Contain[ProductId, ProductName, PictureUrl]
		public int ProductId { get; set; }
		public string ProductName { get; set; }=null!;
		public string PictureUrl { get; set; } = null!;
	}
}