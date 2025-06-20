using AutoMapper;
using CoreLayer.models.Identity;
using Talabat_APIs.Dto.Identity;

namespace Talabat_APIs.Mapping
{
	public class UserMapping:Profile
	{
		public UserMapping()
		{
			CreateMap<AddressDto, Address >().ReverseMap();
			CreateMap<RegisterDto, ApplicationUser>()
				.ForMember(dest => dest.UserName,
					opt => opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf("@"))));
		}
	}
}
