namespace SitrepAPI.Models
{
    /// <summary>
    /// Class of telephone number
    /// </summary>
    public class TelephoneDTO
    {
        /// <summary>
        /// Telephone number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// constructor of class
        /// </summary>
        /// <param name="number">telephone number to set</param>
        public TelephoneDTO(string number)
        {
            Number = number;
        }
    }
}
