using AutoMapper;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;
using WhoWantsToBeAMillionaireGame.Models;
using WhoWantsToBeAMillionaireGame.Models.Admin.AdvertiseModels;

namespace WhoWantsToBeAMillionaireGame.MappingProfiles
{
    public class AdvertiseProfile : Profile
    {
        public AdvertiseProfile()
        {
            CreateMap<Advertise, AdvertiseDto>();
            CreateMap<AdvertiseDto, Advertise>();
            CreateMap<AdvertiseDto, AdvertisePostModel>();
            CreateMap<AdvertisePostModel, AdvertiseDto>();
        }
    }
}
