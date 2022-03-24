namespace SitrepAPI.ResourceParameters
{
    /// <summary>
    /// Query parameters class
    /// </summary>
    public abstract class DefaultResourceParameters
    {
        const int maxPageSize = 30;
        /// <summary>
        /// string to search for
        /// </summary>
        public string? SearchQuery { get; set; }
        /// <summary>
        /// Current pagenumber
        /// </summary>
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        /// <summary>
        /// Amount of items per page
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        /// <summary>
        /// Field(s) to orderby, desc/asc
        /// </summary>
        public abstract string? OrderBy { get; set; }

        /// <summary>
        /// field(s) to return
        /// </summary>
        public string? Fields { get; set; }

        /// <summary>
        ///  {Propertyname}{Operation}{Value}
        ///  StatusId == 2
        ///  valid operations: == >= <![CDATA[<=]]> !=
        /// </summary>
        public string? Filter { get; set; }
    }
}
