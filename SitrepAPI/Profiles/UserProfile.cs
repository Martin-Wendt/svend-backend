using AutoMapper;
using SitrepAPI.Models;
using SitrepAPI.Models.Auth0;

namespace SitrepAPI.Profiles
{
    /// <summary>
    /// Auto Mapper
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// User mapping profiles
        /// </summary>
        public UserProfile()
        {
            CreateMap<Auth0UserDTO, UserDTO>().ForMember(dest => dest.Telephone, opt => opt.MapFrom(x => x.UserMetadata.Telephone));
            CreateMap<Telephone, TelephoneDTO>();
        }
    }
}
