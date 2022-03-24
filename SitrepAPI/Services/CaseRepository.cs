using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SitrepAPI.DbContexts;
using SitrepAPI.Entities;
using SitrepAPI.Helpers;
using SitrepAPI.Models;
using SitrepAPI.ResourceParameters;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Implementation of Case repository service 
    /// </summary>
    public class CaseRepository : BaseRepository, ICaseRepository
    {
        private readonly IMapper _mapper;
        private readonly SitrepDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IUserInformationService _userInformationService;

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="propertyMappingService">Propertymapping service</param>
        /// <param name="mapper">Auto mapper</param>
        /// <param name="userInformationService">User information service</param>
        /// <exception cref="ArgumentNullException">Dependency injection failure</exception>
        public CaseRepository(SitrepDbContext context, IPropertyMappingService propertyMappingService, IMapper mapper, IUserInformationService userInformationService) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userInformationService = userInformationService ?? throw new ArgumentNullException(nameof(userInformationService));
        }

        /// <summary>
        /// <see cref="ICaseRepository.GetCase(int)"/>
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns><see cref="Case"/></returns>
        public Case GetCase(int caseId)
        {
            var @case = _context.Cases.Include(p => p.Priority).Include(s => s.Status).Include(i => i.Images).Include(x => x.Logs).SingleOrDefault(x => x.CaseId == caseId);

            if (@case is null)
            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentNullException(nameof(@case));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }

            return @case;
        }

        /// <summary>
        /// <see cref="ICaseRepository.CaseExists(int)"/>
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns>Boolean</returns>
        public bool CaseExists(int caseId)
        {
            return _context.Cases.Any(x => x.CaseId == caseId);
        }

        /// <summary>
        /// <see cref="ICaseRepository.AddCase(Case)"/>
        /// </summary>
        /// <param name="case">Case to add</param>
        /// <exception cref="ArgumentNullException">Null input value</exception>
        public void AddCase(Case @case)
        {
            if (@case is null)
            {
                throw new ArgumentNullException(nameof(@case));

            }
            _context.Cases.Add(@case);
        }

        /// <summary>
        /// <see cref="ICaseRepository.GetCases(CaseResourceParameters, string)"/>
        /// </summary>
        /// <param name="caseResourceParameters">Query parameters</param>
        /// <param name="userId">Id of user</param>
        /// <returns><see cref="PagedList{T}"/> of <see cref="Case"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public PagedList<Case> GetCases(CaseResourceParameters caseResourceParameters, string userId)
        {
            IQueryable<Case> collection;

            if (caseResourceParameters is null)
            {
                throw new ArgumentNullException(nameof(caseResourceParameters));
            }

            if (userId is null)
            {
                collection = _context.Cases.Include(p => p.Priority).Include(s => s.Status) as IQueryable<Case>;
            }
            else
            {
                collection = _context.Cases.Where(x => x.UserId == userId).Include(p => p.Priority).Include(s => s.Status) as IQueryable<Case>;
            }

            if (!string.IsNullOrWhiteSpace(caseResourceParameters.Filter))
            {
                var casePropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CaseDTO, Case>();

                collection = collection.ApplyFilter(caseResourceParameters.Filter, casePropertyMappingDictionary);
            }

            if (!string.IsNullOrWhiteSpace(caseResourceParameters.SearchQuery))
            {
                var searchQuery = caseResourceParameters.SearchQuery.Trim();

                collection = collection.Where(
                    x => x.Description.Contains(searchQuery) ||
                    x.Location.Contains(searchQuery) ||
                    x.Title.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(caseResourceParameters.OrderBy))
            {
                var casePropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CaseDTO, Case>();

                collection = collection.ApplySort(caseResourceParameters.OrderBy, casePropertyMappingDictionary);
            }

            return PagedList<Case>.Create(collection, caseResourceParameters.PageNumber, caseResourceParameters.PageSize);
        }

        /// <summary>
        /// <see cref="ICaseRepository.UpdateCaseAsync(Case, string)"/>
        /// </summary>
        /// <param name="case">Case to update</param>
        /// <param name="userId">Id of performing user</param>
        public async Task UpdateCaseAsync(Case @case, string userId)
        {
            var modifiedEntities = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

            foreach (var modifiedEntity in modifiedEntities)
            {
                if (modifiedEntity.GetType() == typeof(CaseImage)) continue;

                foreach (var modifiedProperty in modifiedEntity.Properties.Where(x => x.IsModified))
                {
                    //if(modifiedProperty is null){}

                    if (modifiedProperty.OriginalValue != modifiedProperty.CurrentValue)
                    {

                        var userInfo = await _userInformationService.GetUserInformationAsync(userId);
                        CaseLogDTO caseLog = new()
                        {
                            CaseId = @case.CaseId,
                            Message = $"{modifiedProperty.Metadata.Name} ændret fra {modifiedProperty.OriginalValue} til {modifiedProperty.CurrentValue}",
                            CreatedAt = DateTimeOffset.Now,
                            CreatedBy = userInfo.Name,
                            CreatedById = userId
                        };
                        var logsave = _mapper.Map<CaseLog>(caseLog);

                        _context.CaseLogs.Add(logsave);
                    }
                }
            }
        }

        /// <summary>
        /// <see cref="ICaseRepository.Delete(Case)"/>
        /// </summary>
        /// <param name="entityToDelete">object to delete</param>
        public void Delete(Case entityToDelete)
        {
            //
            entityToDelete.StatusId = 6;
        }

        /// <summary>
        /// <see cref="ICaseRepository.GetImageCount(int)"/>
        /// </summary>
        /// <param name="caseId">Id of Case</param>
        /// <returns>Int</returns>
        public int GetImageCount(int caseId)
        {
            return _context.CaseImages.Where(x => x.CaseId == caseId).Select(x => x.CaseImageId).Count();
        }

        /// <summary>
        /// <see cref="ICaseRepository.GetLogCount(int)"/>
        /// </summary>
        /// <param name="caseId">Id of Case</param>
        /// <returns></returns>
        public int GetLogCount(int caseId)
        {
            return _context.CaseLogs.Where(x => x.CaseId == caseId).Select(x => x.CaseLogId).Count();
        }
    }
}
