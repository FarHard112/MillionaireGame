using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsToBeAMillionaireGame.Core.DataTransferObjects
{
    public class ClickedAdDto
    {
        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public string IPAddress { get; set; }
        public DateTime Date { get; set; }
    }
}
