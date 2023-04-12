using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsToBeAMillionaireGame.DataBase.Entities
{
    public class Advertise : IBaseEntity
    {
        public Guid Id { get; set; }
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
        public bool AdStatus { get; set; }

    }
}
