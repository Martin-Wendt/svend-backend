namespace SitrepAPI.Helpers

{
    /// <summary>
    /// extension of list to include paging information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// number of total pages
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// The number of item a page can hold 
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total amount of pages
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Current list has previous page
        /// </summary>
        public bool HasPrevious => (CurrentPage > 1);
        /// <summary>
        /// Current page has next page
        /// </summary>
        public bool HasNext => (CurrentPage < TotalPages);

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="items">List to appened too</param>
        /// <param name="count">number of items</param>
        /// <param name="pageNumber">current page number</param>
        /// <param name="pageSize">current page size</param>
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        /// <summary>
        /// Static exstension method of List to create pagedlist 
        /// </summary>
        /// <param name="source">List to appened too</param>
        /// <param name="pageNumber">Current page number</param>
        /// <param name="pageSize">Current page size</param>
        /// <returns></returns>
        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
