using AutoMapper;
using SitrepAPI.Entities;
using SitrepAPI.Models;

namespace SitrepAPI.Profiles
{
    /// <summary>
    /// Auto Mapper profiles
    /// </summary>
    public class StatusProfile : Profile
    {
        /// <summary>
        /// Status mapping profile
        /// </summary>
        public StatusProfile()
        {
            CreateMap<Status, CaseDTO>();
        }
    }
}
