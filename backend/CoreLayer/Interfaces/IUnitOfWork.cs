using CoreLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		IGenericRepository<T> Repository<T>() where T : BaseClass;
		Task<int> CompleteAsync();

	}
}
