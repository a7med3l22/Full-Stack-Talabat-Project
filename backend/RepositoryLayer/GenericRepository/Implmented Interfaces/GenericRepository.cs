using CoreLayer.Generic_Specification;
using CoreLayer.Interfaces;
using CoreLayer.models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.GenericRepository;
using RepositoryLayer.GenericRepository.Data;
using RepositoryLayer.GenericRepository.Specefication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implmented_Interfaces
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
	{
		private readonly ApplicationContext _context;

		public GenericRepository(ApplicationContext context)
		{
			_context = context;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync(); //AsNoTracking() 
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllSpecAsync(ISpecification<T> spec) //with Tracking
		{
			return await Query(spec).ToListAsync();
		}

		public async Task<T?> GetFirstOrDefaultSpecAsync(ISpecification<T> spec)
		{
		  return await Query(spec).FirstOrDefaultAsync();
		}

		public async Task<int> CountAsync(ISpecification<T> Spec)
		{
			return await Query(Spec).CountAsync();
		}

		private IQueryable<T> Query(ISpecification<T> Spec)
		{
			return SpecificationEvaluator<T>.Query(_context.Set<T>(), Spec);
		}

		public void remove(T entity)
		{
			_context.Remove(entity);
		}

		public void insert(T entity)
		{
			_context.Add(entity);
		}

		public void update(T entity)
		{
			_context.Update(entity);
		}
	}
}
