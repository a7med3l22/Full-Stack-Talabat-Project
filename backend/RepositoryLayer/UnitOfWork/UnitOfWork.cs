using CoreLayer.Interfaces;
using CoreLayer.models;
using RepositoryLayer.GenericRepository;
using RepositoryLayer.Implmented_Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationContext _context;
		private readonly Dictionary<Type,object> _dictionary = new();
		public UnitOfWork(ApplicationContext context)
		{
			_context = context;
		}
		public IGenericRepository<T> Repository<T>() where T : BaseClass
		{
			var key = typeof(T);
			if (!_dictionary.ContainsKey(key))
				_dictionary.Add(key, new GenericRepository<T>(_context));
			return(IGenericRepository<T>)_dictionary[key];
		}

		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			await _context.DisposeAsync();
		}
	}
}
