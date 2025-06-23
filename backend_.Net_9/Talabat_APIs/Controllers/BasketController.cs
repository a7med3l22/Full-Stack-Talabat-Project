using AutoMapper;
using CoreLayer.models.BasketModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.BasketRepository;
using Talabat_APIs.Dto.Basket;
using Talabat_APIs.HandelErrors;

namespace Talabat_APIs.Controllers
{
	public class BasketController : BaseController
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepo,IMapper mapper) 
		{
			_basketRepo = basketRepo;
			_mapper = mapper;
		}
		[HttpGet]
		//return the basket by id and if there is no basket  with this id create basket with the id.
		public async Task<ActionResult<Basket>> GetBasketByIdAsync(string id)
		{
			var basket = await _basketRepo.GetAsync(id);
			return Ok(basket!=null?basket : new Basket(id)); //if basket is not null return the basket else return new basket with the id
		}
		[HttpPost]
		//UpdateOrAdd the basket
		//it the basket UpdateOrAdd successfully return basket else return bad request
		public async Task<ActionResult<Basket>> UpdateOrAddBasketAsync(BasketDto basketDto)
		{
			var basket = _mapper.Map<BasketDto, Basket>(basketDto); //map the basketDto to basket
			var UpdateOrAddBasket = await _basketRepo.UpdateOrAddAsync(basket);
			return UpdateOrAddBasket!=null ? Ok(UpdateOrAddBasket) //if UpdateOrAddBasket is not null return the basket
				: BadRequest(new ApiErrors(400, "Basket not updated or added")); //else return bad request with message
		}
		[HttpDelete]
		//Delete the basket by id
		public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
		{
			var deleteBasket=await _basketRepo.DeleteAsync(id); //delete the basket by id
			return deleteBasket?Ok(deleteBasket) : BadRequest(new ApiErrors(400, "Basket not deleted")); //else return bad request with message                                               //if deleteBasket is true return ok with true else return bad request with message
		}
	}
}
