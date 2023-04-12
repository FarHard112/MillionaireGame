using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoWantsToBeAMillionaireGame.DataBase.Migrations;

namespace WhoWantsToBeAMillionaireGame.DataBase.Entities
{
    public class ClickedAd:IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public string IPAddress { get; set; }
        public DateTime Date { get; set; }
    }
}
