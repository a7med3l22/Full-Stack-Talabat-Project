using CoreLayer.Interfaces;
using CoreLayer.models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Token
{
	public  class JwtToken : IJWTToken
	{
		private readonly IConfiguration _config;

		public  JwtToken(IConfiguration config)
		{
			_config = config;
		}
		public async Task<string> Token(ApplicationUser user, UserManager<ApplicationUser> manager)	
		{
			//Create JWT Token 
			//JWT CREATED fROM {claims,key}

			//create claim
			var claims=new List<Claim>
			{
				new Claim(ClaimTypes.Email,user.Email!),
				new Claim(ClaimTypes.NameIdentifier, user.Id)
			};
			var roles =await manager.GetRolesAsync(user);
			foreach(var role in roles)
			{
				new Claim(ClaimTypes.Role, role);
			}
			//create Key
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]!));
			var signingCredentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);

			var jwttoken = new JwtSecurityToken(
				issuer: _config["JWT:issuer"],
				audience: _config["JWT:audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddDays(double.Parse(_config["JWT:expiresInDays"]!)),
				signingCredentials: signingCredentials
				);

			// ده كده هيعملي ال توكين بناء ع المعلومات اللي دخلتها دي 
			return new JwtSecurityTokenHandler().WriteToken(jwttoken);

		}

		
	}
}
