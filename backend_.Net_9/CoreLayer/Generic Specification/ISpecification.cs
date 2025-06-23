using CoreLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Generic_Specification
{
	public interface ISpecification<T> where T : BaseClass
	{
		//_context.Products.Include(p=>p.Brand).Include(p=>p.Category).ToListAsync()
		//_context.Products.Include(p => p.Brand).Include(p => p.Category).Where(p=>p.Id==id).FirstOrDefaultAsync()

		 List<Expression<Func<T,object>>> Includes { get; set; }
		 Expression<Func<T,bool>>? Filter { get; set; }

		Expression<Func<T, object>>? OrderBy { get; set; }
		 Expression<Func<T, object>>? OrderByDesc { get; set; }
		 int Take { get; set; } // for pagination
		 int Skip { get; set; } // for pagination
		bool IsPagingEnabled { get; set; } // for pagination

	}
}
