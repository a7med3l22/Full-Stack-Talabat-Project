using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Generic_Specification.ProductSpecefication
{
	public class ProductParametersinGetall
	{
		public string? sort { get; set; }
		public int? categoryId { get; set; }
		public int? brandId { get; set; }


		//PageSize,PageIndex,Search


		private int _pageSize = 6; // القيمة الافتراضية لو مبعتش حاجة هيخليها كده ولو بعت هيدخل علي ال set غير كده مش هيدخل
		public int PageSize
		{
			get => _pageSize;
			set
			{
				if (value > 10)
					_pageSize = 6;  // لو أكبر من 10، يرجّعها 6
				else if (value < 1)
					_pageSize = 1;  // لو أقل من 1، يرجّعها 1
				else
					_pageSize = value;
			}
		}

		private int _pageIndex = 1; // القيمة الافتراضية
		public int PageIndex
		{
			get => _pageIndex;
			set
			{
				if (value < 1)
					_pageIndex = 1;
				else
					_pageIndex = value;
			}
		}

		private string? search;

		public string? Search
		{
			get { return search?.ToLower(); }
			set { search = value; }
		}

	}
}