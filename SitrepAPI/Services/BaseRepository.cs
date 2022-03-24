using SitrepAPI.DbContexts;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Base repository class
    /// </summary>
    public abstract class BaseRepository
    {
        private readonly SitrepDbContext _context;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BaseRepository(SitrepDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        /// <summary>
        /// Save changes made to dbcontext
        /// </summary>
        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}