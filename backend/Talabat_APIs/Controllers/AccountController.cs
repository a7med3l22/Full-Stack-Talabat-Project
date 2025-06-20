using AutoMapper;
using CoreLayer.Interfaces;
using CoreLayer.models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IdentityRepository;
using ServiceLayer.Token;
using System.Security.Claims;
using Talabat_APIs.Dto.Identity;
using Talabat_APIs.Extentions;
using Talabat_APIs.HandelErrors;

namespace Talabat_APIs.Controllers
{
	public class AccountController : BaseController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
		private readonly IJWTToken _token;
		private readonly SignInManager<ApplicationUser> _login;

		public AccountController(UserManager<ApplicationUser> userManager,IMapper mapper,IJWTToken token,SignInManager<ApplicationUser> login)
		{
			_userManager = userManager;
			_mapper = mapper;
			_token = token;
			_login = login;
		}
		//i want to make register action first 
		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> AccountResgister(RegisterDto RegisterDto)
		{
			//1- i want to save Register User to database
			var user = new ApplicationUser();
			_mapper.Map(RegisterDto, user); // جاااااااااااااااااامد عاش كمل 
			
		 var result=await _userManager.CreateAsync(user,RegisterDto.Password);//// جامد عاش اووووي

			if (!result.Succeeded)
			{
				return BadRequest(new validationError(result.Errors.Select(e => e.Description).ToList()));
			}
			var userDto = new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await _token.Token(user, _userManager)
			};
			return Ok(userDto);
		}

		//Login	
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto login)
		{

			//check if email is correct 
			var user=await _userManager.FindByEmailAsync(login.Email);
			if (user == null) return Unauthorized(new ApiErrors(400, "Login Failed"));
			//check if Password is correct 
			var CheckPasssword = await _login.CheckPasswordSignInAsync(user, login.Password, false);
			if (!CheckPasssword.Succeeded)
			{
				return Unauthorized(new ApiErrors(400, "Login Failed"));
			}

			var userDto = new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await _token.Token(user, _userManager)
			};
			return Ok(userDto);
		}

		//Get Current User
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			//get userId from (Claims In  JWT token == User )

			//var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			//if (userId == null) return Unauthorized(new ApiErrors(401));
			//var user=await _userManager.FindByIdAsync(userId);
			/////These 3 Line Equal next Line
			var user=await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized(new ApiErrors(401));
			var userDto = new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await _token.Token(user, _userManager)
			};
			return Ok(userDto);
		}
		//Get Current User Address 
		[HttpGet("GetUserAddress")]
		[Authorize]
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			//Get Current User By Token Claims
			//var user= await _userManager.GetUserAsync(User);
			var UserIncludeAddress = await _userManager.GetUserWithAddressAsync(User);//_userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u=>u.Id== userId);
			if (UserIncludeAddress == null) return Unauthorized(new ApiErrors(401));
			if(UserIncludeAddress.Address == null) return NotFound(new ApiErrors(404));
			var AddressDto=_mapper.Map<Address,AddressDto>(UserIncludeAddress.Address);	
			return Ok(AddressDto);
		}
		//--UpdateUserAddress
		[HttpPut]
		[Authorize]
		public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto newAddress)
		{
			//get current userwith Address
			var user= await _userManager.GetUserWithAddressAsync(User);
			if (user == null) return Unauthorized(new ApiErrors(401));
			if(user.Address==null)
			{
				user.Address=new Address();
			}
			//thus we insure that every user have an object of address
			_mapper.Map(newAddress, user.Address);
			//we want to update User
			var updatedUser =  await _userManager.UpdateAsync(user);
			if (!updatedUser.Succeeded) return BadRequest(new validationError(updatedUser.Errors.Select(e=>e.Description).ToList()));
			var AddressDto = _mapper.Map<AddressDto>(user.Address);
			return Ok(AddressDto);
		}

	}
}
