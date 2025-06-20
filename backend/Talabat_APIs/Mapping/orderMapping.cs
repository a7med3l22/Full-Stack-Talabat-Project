using AutoMapper;
using CoreLayer.models.Order;
using Talabat_APIs.Dto.OrderDto;

namespace Talabat_APIs.Mapping
{
	public class orderMapping:Profile
	{
		public orderMapping()
		{
			CreateMap<Order,orderToReturnDto>()
				.ForMember(dest=>dest.Items,opt=>opt.MapFrom(src=>src.orderItems))
				.ForMember(dest => dest.deliveryMethodCost, opt => opt.MapFrom(src => src.deliveryMethod != null ? src.deliveryMethod.Cost : 0))
				;

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest=>dest.ProductId,opt=>opt.MapFrom(src=>src.productItemOrdered.ProductId))
				.ForMember(dest=>dest.ProductName,opt=>opt.MapFrom(src=>src.productItemOrdered.ProductName))
				.ForMember(dest=>dest.PictureUrl,opt=>opt.MapFrom<PictureOrderResolver>())
				;
		}
	}
}
