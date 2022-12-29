using AutoMapper;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;
using WhoWantsToBeAMillionaireGame.Models;

namespace WhoWantsToBeAMillionaireGame.MappingProfiles;

public class PrizeProfile:Profile
{
    public PrizeProfile()
    {
        CreateMap<PrizeModel, PrizeComboBoxDto>();
        CreateMap<PrizeComboBoxDto, PrizeModel>();

        CreateMap<PrizeDto, Prize>();
        CreateMap<Prize, PrizeDto>();

        CreateMap<PrizeComboBoxDto, Prize>();
        CreateMap<Prize, PrizeComboBoxDto>();

        CreateMap<PrizePostModel, PrizeDto>();
        CreateMap<PrizeDto, PrizePostModel>();

    }
}