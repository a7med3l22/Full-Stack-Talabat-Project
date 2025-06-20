using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Generic_Specification.Order_Specefication
{
	public class SpecParams
	{
		public string? Email { get; set; }
		public int? orderId { get; set; }
		public string? PaymentIntentId { get; set; }
	}
}
