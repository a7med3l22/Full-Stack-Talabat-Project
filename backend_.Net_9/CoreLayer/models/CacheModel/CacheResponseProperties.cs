using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.models.CacheModel
{
	public class CacheResponseProperties
	{
		public required string Data { get; set; }
		public int StatusCode { get; set; }
	}
}
