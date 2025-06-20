using AutoMapper;
using AutoMapper.Execution;
using CoreLayer.models.ProductModels;
using Talabat_APIs.Dto.Product;

namespace Talabat_APIs.Mapping
{
	public class MappingPictureURL : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _config;

		public MappingPictureURL(IConfiguration config)
		{
			_config = config;
		}
		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			
			destination.PictureUrl = $"{_config["ApiUrl"]}{source.PictureUrl}"; 
			return destination.PictureUrl;
		}
	}
}
