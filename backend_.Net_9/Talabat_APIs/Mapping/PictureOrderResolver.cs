using AutoMapper;
using CoreLayer.Interfaces;
using CoreLayer.models.Order;
using Microsoft.IdentityModel.Tokens;
using Talabat_APIs.Dto.OrderDto;

namespace Talabat_APIs.Mapping
{
	public class PictureOrderResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;
		private readonly IApiUrl _apiUrl;

		public PictureOrderResolver(IConfiguration configuration,IApiUrl apiUrl)
		{
			_configuration = configuration;
			_apiUrl = apiUrl;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.productItemOrdered.PictureUrl))
			{
				return _apiUrl.GetApiUrl() + source.productItemOrdered.PictureUrl;
			}
			return string.Empty;
		}
	}
}