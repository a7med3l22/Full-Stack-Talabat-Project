using AutoMapper;
using CoreLayer.models.ProductModels;
using Talabat_APIs.Dto.Product;

namespace Talabat_APIs.Mapping
{
	public class ProductMapping:Profile
	{
		public ProductMapping()
		{
			CreateMap<Product,ProductToReturnDto>()	
			.ForMember(dest=>dest.ProductBrand, opt=>opt.MapFrom(src=>src.Brand.Name))
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
			.ForMember(dest => dest.Category, opt => opt.MapFrom<MappingPictureURL>());

		}
	}
}
