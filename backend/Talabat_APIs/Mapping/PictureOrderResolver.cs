using AutoMapper;
using CoreLayer.models.Order;
using Microsoft.IdentityModel.Tokens;
using Talabat_APIs.Dto.OrderDto;

namespace Talabat_APIs.Mapping
{
	public class PictureOrderResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public PictureOrderResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.productItemOrdered.PictureUrl))
			{
				return _configuration["ApiUrl"] + source.productItemOrdered.PictureUrl;
			}
			return string.Empty;
		}
	}
}