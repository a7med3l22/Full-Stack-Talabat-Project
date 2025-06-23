using CoreLayer.Interfaces;
using CoreLayer.models.CacheModel;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Cache_Service
{
	public class ResponseCacheService : IResponseCacheService
	{
		private readonly IDatabase _dataBase;
		public ResponseCacheService(IConnectionMultiplexer connection)
		{
			_dataBase=connection.GetDatabase();
		}
		public async Task CreateCacheAsync(string cacheKey, CacheResponseProperties cahceResponseProperties,TimeSpan timeToLife)
		{
			var jsonStringResponse=JsonSerializer.Serialize(cahceResponseProperties, JsonOptions); //convert it to string json
			await _dataBase.StringSetAsync(cacheKey, jsonStringResponse, timeToLife);
		}
		public async Task<CacheResponseProperties?> GetCacheAsync(string cacheKey)
		{
			var Cache=await _dataBase.StringGetAsync(cacheKey);
			if (Cache.IsNullOrEmpty) return null;
			return JsonSerializer.Deserialize<CacheResponseProperties>(Cache!, JsonOptions); //return it as CacheResponseProperties //we use JsonOptions to search in Cache! with property data not Data search in it as camel not pascal !
		}
		public JsonSerializerOptions JsonOptions => _jsonOptions; //this will make one instance of JsonSerializerOptions <3

		private static readonly JsonSerializerOptions _jsonOptions = new()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

	}
}
