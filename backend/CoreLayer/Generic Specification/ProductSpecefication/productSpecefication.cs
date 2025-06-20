using CoreLayer.Generic_Specification.ProductSpecefication;
using CoreLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Generic_Specification
{
	public class productSpecefication: FilterProductSpec
	{
		//_context.Products.Include(p=>p.Brand).Include(p=>p.Category).ToListAsync()
		//_context.Products.Include(p => p.Brand).Include(p => p.Category).Where(p=>p.Id==id).FirstOrDefaultAsync()



		//i want to sort products by Name,PriceAsc,PriceDesc and make default is Name
		//i want to Filter By CategoryId&&BrandId -- where(p=>p.CategoryId==productParams.categoryId)
		//i want to use pagination-- skip,take
		//i want to use search -- where(p=>p.Name.Contains(productParams.searchString))
		public productSpecefication(ProductParametersinGetall productParams):base(productParams)
		{
			includes();
			int pageIndex = productParams.PageIndex; 
			int pageSize = productParams.PageSize;  // default to 10 if not provided
			pagination((pageIndex - 1) * pageSize, pageSize);

			if (!string.IsNullOrEmpty(productParams.sort))
			{
				switch (productParams.sort)
				{
					case "name":
						OrderBy = (p => p.Name);
						break;
					case "priceAsc":
						OrderBy = (p => p.Price);
						break;
					case "priceDesc":
						OrderByDesc = (p => p.Price); // to sort by descending
						break;
					default:
						OrderBy = (p => p.Name); // default sorting by Name
						break;
				}
			}
			else
			{
				OrderBy = (p => p.Name); // default sorting by Name
			}
		}

		public productSpecefication(int id):base(p => p.Id == id)
		{
			includes();
		}

		private void includes()
		{
			Includes.Add(p => p.Category);
			Includes.Add(p => p.Brand);
		}
	}
}
