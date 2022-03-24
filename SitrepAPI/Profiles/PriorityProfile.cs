using AutoMapper;

namespace SitrepAPI.Profiles
{
    /// <summary>
    /// Auto Mapper profiles
    /// </summary>
    public class PriorityProfile : Profile
    {
        /// <summary>
        /// Priority mapping profiles
        /// </summary>
        public PriorityProfile()
        {
            CreateMap<Entities.Priority, Models.CaseDTO>();
        }
    }
}
