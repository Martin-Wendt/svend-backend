using AutoMapper;

namespace SitrepAPI.Profiles
{
    /// <summary>
    /// Auto Mapper Profiles
    /// </summary>
    public class CaseProfile : Profile
    {
        /// <summary>
        /// Case mapping Profiles
        /// </summary>
        public CaseProfile()
        {
            CreateMap<Models.CaseDTO, Entities.Case>();
            CreateMap<Entities.Case, Models.CaseDTO>()
                .IncludeMembers(p => p.Priority, s => s.Status)
                .ForMember(
                dest => dest.ImageCount,
                opt => opt.MapFrom(x => x.Images.Count))
                .ForMember(
                dest => dest.LogCount,
                opt => opt.MapFrom(x => x.Logs.Count))
                .ForMember(
                dest => dest.CaseImages,
                opt => opt.MapFrom(x => x.Images))
                .ForMember(
                dest => dest.LatestChangeAt,
                opt => opt.MapFrom((src, dest) =>
                {
                    if (src.Logs.Count > 0)
                    {
                        return src.Logs.OrderByDescending(x => x.CreatedAt).First().CreatedAt;
                    }
                    return src.CreatedAt;
                }));

        CreateMap<Models.CaseForCreationDTO, Entities.Case>()
                .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(
                dest => dest.StatusId,
                opt => opt.MapFrom(src => 1))
                .ForMember(
                dest => dest.PriorityId,
                opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<Entities.Case, Models.CaseForUpdateDTO>();
            CreateMap<Models.CaseForUpdateDTO, Entities.Case>();
        }
}
}
