using CoreLayer.models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Generic_Specification.ProductSpecefication
{
	public class FilterProductSpec:BaseSpecefication<Product>
	{
		public FilterProductSpec(ProductParametersinGetall productParams) :base(p => (!productParams.categoryId.HasValue || p.CategoryId == productParams.categoryId) && (!productParams.brandId.HasValue || p.BrandId == productParams.brandId)&&(string.IsNullOrEmpty(productParams.Search)||p.Name.ToLower().Contains(productParams.Search)))
		{
			
		}
		public FilterProductSpec(Expression<Func<Product,bool>> expression):base(expression)
		{
			
		}
	}
}
