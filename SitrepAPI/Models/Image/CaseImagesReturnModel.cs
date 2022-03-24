namespace SitrepAPI.Models
{
#pragma warning disable
    /// <summary>
    /// Representation of model when requesting multiple images
    /// </summary>
    public class CaseImagesReturnModel
    {
        public int CaseImageId { get; set; }
        public LinkDTO Links { get; set; }
    }
#pragma warning restore
}
