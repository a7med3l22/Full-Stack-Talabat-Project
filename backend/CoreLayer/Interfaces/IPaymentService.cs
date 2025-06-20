using CoreLayer.models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
	public interface IPaymentService
	{
		Task<Basket?> CreateOrUpdatePaymentIntent(string basketId);
		Task OrderPaymentStatusAsync(string PaymentIntentId, bool IsSucceeded);

	}
}
