using CoreLayer.Generic_Specification;
using CoreLayer.Generic_Specification.ProductSpecefication;
using CoreLayer.Interfaces;
using CoreLayer.models.ProductModels;
using RepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Entity_Service
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IReadOnlyList<Brand>> GetBrandsAsync()
		{
			var Brands = await _unitOfWork.Repository<Brand>().GetAllAsync();
			return Brands;
		}

		public async Task<IReadOnlyList<Category>> GetCategorysAsync()
		{
			var Categorys = await _unitOfWork.Repository<Category>().GetAllAsync();
			return Categorys;
		}

		public async Task<Product?> GetProductByIdAsync(int productId)
		{
			var productSpec = new productSpecefication(productId);
			var product = await _unitOfWork.Repository<Product>().GetFirstOrDefaultSpecAsync(productSpec);
			return product;
		}

		public async Task<int> GetProductCountAsync(ProductParametersinGetall productParams)
		{
			var countSpec = new FilterProductSpec(productParams);
			var count = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
			return count;
		}

		public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductParametersinGetall productParams)
		{
			var productSpec = new productSpecefication(productParams);
			var products = await _unitOfWork.Repository<Product>().GetAllSpecAsync(productSpec);
			return products;
		}
	}
}
