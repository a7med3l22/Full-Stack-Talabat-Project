using AutoMapper;
using CoreLayer.Generic_Specification.ProductSpecefication;
using CoreLayer.Interfaces;
using CoreLayer.models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Talabat_APIs.Dto.Product;
using Talabat_APIs.HandelErrors;
using Talabat_APIs.HelperAttribute;
using Talabat_APIs.Paginations;

namespace Talabat_APIs.Controllers
{
	public class ProductController : BaseController
	{

		private readonly IMapper _mapper;
		private readonly IProductService _productService;

		public ProductController(IMapper mapper,IProductService productService)
		{
			
			_mapper = mapper;
			_productService = productService;
		}
		[Cache(3)]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrors))]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(paginationResponse<ProductToReturnDto>))]
		public async Task<ActionResult<paginationResponse<ProductToReturnDto>>> GetAllProduct([FromQuery]ProductParametersinGetall productParams)
		{
			//-- Implement to sort by Name, PriceAsc, PriceDesc //sort="PriceAsc" <Done>
			//-- Filter By CategoryId&&BrandId  <Done>
			//--implement pagination  <Done>
			//-- i need response to be like this --pageIndex ,pageSize,count,data <Done>
			//--i need to enable count  <Done>
			//--ineed to enable search <Done> 

			var products = await _productService.GetProductsAsync(productParams);
			if (products == null)
			{
				return NotFound();
			}
			var ProdectDto= _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

			var count = await _productService.GetProductCountAsync(productParams);
			var paginationResponse = new paginationResponse<ProductToReturnDto>
			{
				pageIndex = productParams.PageIndex,
				pageSize = productParams.PageSize,
				data = ProdectDto,
				count = count
			};
			return Ok(paginationResponse);
		}
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrors))]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductToReturnDto))]
		public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				//return NotFound(new ApiErrors(404));
				 throw new NullReferenceException();
			}
			var productDto=_mapper.Map<Product,ProductToReturnDto>(product);
			return Ok(productDto);
		}

		//getBrands
		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrands()
		{
			var Brands=await _productService.GetBrandsAsync();
			return Ok(Brands);
		}
		//getCategory
		[HttpGet("Categories")]
		public async Task<ActionResult<IReadOnlyList<Category>>> GetCategory()
		{
			var Category=await _productService.GetCategorysAsync();
			return Ok(Category);
		}
	}
}
