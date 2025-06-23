using CoreLayer.Generic_Specification;
using CoreLayer.Generic_Specification.ProductSpecefication;
using CoreLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
	public interface IGenericRepository<T> where T : BaseClass
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);

		Task<IReadOnlyList<T>> GetAllSpecAsync(ISpecification<T> spec);
		Task<T?> GetFirstOrDefaultSpecAsync(ISpecification<T> spec);
		Task<int> CountAsync(ISpecification<T> countSpec);
	
		void remove(T entity);
		void insert(T entity);
		void update(T entity);
	}
}
