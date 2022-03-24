using AutoMapper;

namespace SitrepAPI.Profiles
{
    /// <summary>
    /// Auto Mapper Profiles
    /// </summary>
    public class CaseLogProfile : Profile
    {
        /// <summary>
        /// CaseLog Mapping profiles
        /// </summary>
        public CaseLogProfile()
        {
            //CreateMap<Entities.CaseLog, Models.CaseLogForCreationDTO>();

            CreateMap<Models.CaseLogForCreationDTO, Entities.CaseLog>()
                .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(d => DateTimeOffset.Now));

            CreateMap<Entities.CaseLog, Models.CaseLogDTO>();

            CreateMap<Models.CaseLogDTO, Entities.CaseLog>();


        }
    }
}
