using CoreLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Generic_Specification
{
	public class BaseSpecefication<T> : ISpecification<T> where T : BaseClass
	{
		//_context.Products.Include(p=>p.Brand).Include(p=>p.Category).ToListAsync()
		//_context.Products.Include(p => p.Brand).Include(p => p.Category).Where(p=>p.Id==id).FirstOrDefaultAsync()
		public List<Expression<Func<T, object>>> Includes { get; set; } = new();
		public Expression<Func<T, bool>>? Filter {get; set;}
		public Expression<Func<T, object>>? OrderBy { get; set; } 
		public Expression<Func<T, object>>? OrderByDesc { get; set; } 
		public int Take { get; set; } // for pagination
		public int Skip { get; set; } // for pagination
		public bool IsPagingEnabled { get; set; } // for pagination

		public BaseSpecefication() // في حالة ان مفهاش where
		{
			
		}
		public BaseSpecefication(Expression<Func<T, bool>>? filter)// لو فيها where
		{
			Filter=filter;
		}
		public void pagination(int skip,int take)
		{
			IsPagingEnabled = true;
			Skip = skip;
			Take = take;
		}

	}
}
