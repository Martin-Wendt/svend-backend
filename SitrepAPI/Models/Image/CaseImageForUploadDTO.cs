using System.ComponentModel.DataAnnotations;

namespace SitrepAPI.Models
{
#pragma warning disable
    /// <summary>
    /// Representation of CaseImage when uploaded
    /// </summary>
    public class CaseImageForUploadDTO
    {
        /// <summary>
        /// Id of case image is related to
        /// </summary>
        public int? CaseId { get; set; }
        /// <summary>
        /// Name of image
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Image in array of bytes
        /// </summary>
        [Required]
        public byte[] Image { get; set; }
        /// <summary>
        /// Type/extention of image
        /// </summary>
        [Required]
        public string Type { get; set; }
    }
#pragma warning restore
}
