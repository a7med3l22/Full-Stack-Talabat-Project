using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.models.Identity
{
	public class ApplicationUser:IdentityUser
	{
		public string DisplayName { get; set; } = null!;
		// every application user may have address
		public Address? Address { get; set; }
		
	}
}
