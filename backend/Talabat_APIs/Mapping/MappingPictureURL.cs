using AutoMapper;
using AutoMapper.Execution;
using CoreLayer.Interfaces;
using CoreLayer.models.ProductModels;
using Microsoft.AspNetCore.Http;
using Talabat_APIs.Dto.Product;

namespace Talabat_APIs.Mapping
{
	public class MappingPictureURL : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _config;
		private readonly IApiUrl _apiUrl;

		public MappingPictureURL(IConfiguration config,IApiUrl apiUrl)
		{
			_config = config;
			_apiUrl = apiUrl;
		}
		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			var apiUrl = _apiUrl.GetApiUrl(); // Assuming GetApiUrl() returns the base URL of the API
			destination.PictureUrl = $"{apiUrl}{source.PictureUrl}"; 
			return destination.PictureUrl;
		}
	}
}
