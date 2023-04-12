namespace WhoWantsToBeAMillionaireGame.Models.Admin.AdvertiseModels
{
    public class AdvertisePostModel
    {    
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string TargetUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Impressions { get; set; }
        public int? Clicks { get; set; }
        public int? AdvertiserId { get; set; }
        public string AdType { get; set; }
        public string AdSize { get; set; }
        public string AdPlacement { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
