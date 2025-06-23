using AutoMapper;
using CoreLayer.models.BasketModels;
using Talabat_APIs.Dto.Basket;

namespace Talabat_APIs.Mapping
{
	public class MappingBasket:Profile
	{
		public MappingBasket()
		{
			CreateMap<BasketDto, Basket>();
			CreateMap<BasketItemsDto, BasketItems>();
		}
	}
}
