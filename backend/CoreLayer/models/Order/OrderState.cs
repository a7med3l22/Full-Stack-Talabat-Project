using System.Runtime.Serialization;

namespace CoreLayer.models.Order
{
	public enum OrderState
	{
		Pending,
		[EnumMember(Value = "Payment Successed")]
		PaymentReceived,
		[EnumMember(Value = "Payment Failed")]
		PaymentFailed
	}
}