using CoreLayer.models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
	public interface IJWTToken
	{
		public Task<string> Token(ApplicationUser user, UserManager<ApplicationUser> manager);	//i create toke with role and user
	}
}
