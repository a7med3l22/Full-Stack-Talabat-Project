using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.models.Identity
{
	public class Address:BaseClass
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Street { get; set; } = null!;
		public string City { get; set; } = null!;
		public string Country { get; set; } = null!;
		//every address is Must haven by one application user
		public ApplicationUser ApplicationUser { get; set; } = null!;
		// So The Address Must Have ForignKey Because It Dependes On ApplicationUser
		public string ApplicationUser_Id { get; set; } = null!;

	}
}
