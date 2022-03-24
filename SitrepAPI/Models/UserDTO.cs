namespace SitrepAPI.Models
{
    /// <summary>
    /// Data transfor object of a User
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Email of user
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Name of user
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Nickname of user
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// Picture chosen by user
        /// </summary>
        public string? Picture { get; set; }
        /// <summary>
        /// Telephone numbers of user
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TelephoneDTO Telephone { get; set; } 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

}
