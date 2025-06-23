using CoreLayer.models.BasketModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RepositoryLayer.BasketRepository
{
	public class BasketRepository : IBasketRepository
	{
		// I want To Add Methods To UpdateOrAdd, Get and Delete Basket
		private readonly IDatabase _redisDatabase;
		public BasketRepository(IConnectionMultiplexer connection)
		{
			var redisDatabase = connection.GetDatabase();
			_redisDatabase=redisDatabase;
		}
		public async Task<Basket?> UpdateOrAddAsync(Basket basket)
		{
			var redisBasket = JsonSerializer.Serialize(basket, JsonSerializerOptions); // Serialize the basket to json string
			var UpdateOrAddBasket =await _redisDatabase.StringSetAsync(basket.Id, redisBasket, TimeSpan.FromDays(7));//return true or false
			return UpdateOrAddBasket?await GetAsync(basket.Id) : null; //if true return the basket else return null
		}
		public async Task<Basket?> GetAsync(string id)
		{
			var GetBasket = await _redisDatabase.StringGetAsync(id); //return null or Basket
			return GetBasket.IsNullOrEmpty?null:JsonSerializer.Deserialize<Basket>(GetBasket!, JsonSerializerOptions); //if null return null else deserialize to Basket object

		}
		public async Task<bool> DeleteAsync(string id)
		{
			var DeleteBasket =await _redisDatabase.KeyDeleteAsync(id); //return true or false
			return DeleteBasket; //if true deleted else not deleted
		}
		private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase

		};
	}
}
