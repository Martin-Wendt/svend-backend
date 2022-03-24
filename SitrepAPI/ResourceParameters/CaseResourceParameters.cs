namespace SitrepAPI.ResourceParameters
{
    /// <summary>
    /// Case specific resource parameters
    /// </summary>
    public class CaseResourceParameters : DefaultResourceParameters
    {
        private string? _orderBy;
        /// <summary>
        /// field(s) to orderby
        /// </summary>
        public override string? OrderBy
        {
            get { return _orderBy; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _orderBy = value;
                }
                else
                {
                    _orderBy = "Title";
                }

            }
        }
    }
}
