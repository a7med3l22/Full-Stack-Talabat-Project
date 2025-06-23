using Talabat_APIs.Dto;

namespace Talabat_APIs.Paginations
{
	public class paginationResponse<T> where T : class
	{	
		public int? pageIndex { get; set; }
		public int ?pageSize { get; set; }
		public int ?count { get; set; }
		public IReadOnlyList<T>? data { get; set; }
	}
}
