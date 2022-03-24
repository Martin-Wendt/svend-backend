using AutoMapper;

namespace SitrepAPI.Profiles
{
    /// <summary>
    /// Auto mapper profile
    /// </summary>
    public class CaseImageProfile : Profile
    {
        /// <summary>
        /// CaseImage mapping profiles
        /// </summary>
        public CaseImageProfile()
        {
            CreateMap<Entities.CaseImage, Models.CaseImageDTO>();

            CreateMap<Models.CaseImageDTO, Entities.CaseImage>();

            CreateMap<Models.CaseImageForUploadDTO, Entities.CaseImage>()
                .ForMember(
                    dest => dest.Size,
                    opt => opt.MapFrom(x => x.Image.LongLength))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(d => DateTimeOffset.Now));

            CreateMap<int, Models.CaseImageDTO>()
                .ForMember(
                dest => dest.CaseImageId,
                opt => opt.MapFrom(x => x));
        }
    }
}
