using CoreLayer.models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Talabat_APIs.Extentions
{
	public static class FindUserWithAddress
	{
		public static async Task<ApplicationUser?> GetUserWithAddressAsync(this UserManager<ApplicationUser> manager,ClaimsPrincipal user)
		{
			var userId=user.FindFirstValue(ClaimTypes.NameIdentifier);
			if(userId == null) { return null; }
			var userIncludeAddress=await manager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Id==userId);
			return userIncludeAddress;
		}
	}
}
