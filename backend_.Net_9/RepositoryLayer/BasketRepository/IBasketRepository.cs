using CoreLayer.models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.BasketRepository
{
	public interface IBasketRepository
	{
		//Iwant To Add Methods To UpdateOrAdd, Get and Delete Basket 

		//UpdateOrAdd
		Task<Basket?> UpdateOrAddAsync(Basket basket);
		//Get
		Task<Basket?> GetAsync(string id);
		//Delete
		Task<bool> DeleteAsync(string id);
	}
}
