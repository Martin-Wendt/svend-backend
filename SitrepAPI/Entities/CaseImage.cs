using System;

namespace SitrepAPI.Entities
{
    /// <summary>
    /// CaseImage database entity
    /// </summary>
    public class CaseImage
    {
        /// <summary>
        /// Id of Image
        /// </summary>
        public int CaseImageId { get; set; }
        /// <summary>
        /// Id of case the image is related to
        /// </summary>
        public int? CaseId { get; set; }
        /// <summary>
        /// Navigational property of related case
        /// </summary>
        public Case? Case { get; set; }
        /// <summary>
        /// Name of image
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// type/extension of Image
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// image Byte array/ actual image
        /// </summary>
        public byte[] Image { get; set; }
        /// <summary>
        /// Time of creation
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// size of image
        /// </summary>
        public long Size { get; private set; }
        /// <summary>
        /// Constructor of class
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CaseImage()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
