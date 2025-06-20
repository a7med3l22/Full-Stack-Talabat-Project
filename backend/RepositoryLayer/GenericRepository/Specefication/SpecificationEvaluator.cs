using CoreLayer.Generic_Specification;
using CoreLayer.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.GenericRepository.Specefication
{
	public static class SpecificationEvaluator<T> where T : BaseClass
	{
		//_context.Set<T>.Include(p=>p.Brand).Include(p=>p.Category)هتبني لحد هنا 
		//_context.Set<T>.Include(p => p.Brand).Include(p => p.Category).Where(p=>p.Id==id)  هتبني لحد هنا 


		//Now spec has OrderBy && OrderByDesc
		public static IQueryable<T> Query(IQueryable<T>inputQuery,ISpecification<T> spec )
		{
			var query = inputQuery; //_context.Set<T>
			if(spec.Filter is not null)
			{
				query = query.Where(spec.Filter); //_context.Set<T>.Where(p=>p.Id==id)
			}

			query = spec.Includes.Aggregate(query,(currentQuery,include)=>currentQuery.Include(include)); //_context.Set<T>.Include(p => p.Brand).Include(p => p.Category).Where(p=>p.Id==id)


			if(spec.OrderBy is not null)
			{
				query = query.OrderBy(spec.OrderBy); //_context.Set<T>.OrderBy(p=>p.Id)
			}
			else if(spec.OrderByDesc is not null)
			{
				query = query.OrderByDescending(spec.OrderByDesc); //_context.Set<T>.OrderByDescending(p=>p.Id)
			}
			
			if(spec.IsPagingEnabled)
			{
				query = query.Skip(spec.Skip).Take(spec.Take); //_context.Set<T>.Take(10).Skip(20)
			}

			return query;
		}
	}
}
