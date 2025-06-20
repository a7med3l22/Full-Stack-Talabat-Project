using System.ComponentModel.DataAnnotations;

namespace Talabat_APIs.Dto.Basket
{
	public class BasketItemsDto
	{
		public int Id { get; set; } 
		public string ProductName { get; set; } = null!;
		public string PictureUrl { get; set; } = null!;
		[Range(0.1, double.MaxValue, ErrorMessage = "Price Must Be Greter Than 0")]

		public decimal Price { get; set; }
		[Range(1,int.MaxValue,ErrorMessage = "Quantity Must Be Greter Than 0")]
		public int Quantity { get; set; }
		public string Brand { get; set; } = null!;
		public string Category { get; set; } = null!;
	}
}