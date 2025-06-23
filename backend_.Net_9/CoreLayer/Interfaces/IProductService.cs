using CoreLayer.Generic_Specification.ProductSpecefication;
using CoreLayer.models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
	public  interface IProductService
	{
		Task<IReadOnlyList<Product>> GetProductsAsync(ProductParametersinGetall productParams);
		Task<int> GetProductCountAsync(ProductParametersinGetall productParams);
		Task<Product?> GetProductByIdAsync(int productId);
		Task<IReadOnlyList<Brand>> GetBrandsAsync();
		Task<IReadOnlyList<Category>> GetCategorysAsync();

	}
}
