using CoreLayer.models.CacheModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
	public interface IResponseCacheService
	{
		Task CreateCacheAsync(string cacheKey, CacheResponseProperties cahceResponseProperties, TimeSpan timeToLife);
		Task<CacheResponseProperties?> GetCacheAsync(string cacheKey);
		JsonSerializerOptions JsonOptions { get; }
	}
}
