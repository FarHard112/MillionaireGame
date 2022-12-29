using AutoMapper;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;
using WhoWantsToBeAMillionaireGame.Models;

namespace WhoWantsToBeAMillionaireGame.MappingProfiles;

public class PrizeColorProfile:Profile
{
    public PrizeColorProfile()
    {
        CreateMap<ColorPrize, PrizeColorDto>();
        CreateMap<PrizeColorDto, ColorPrize>();

        CreateMap<PrizeColorDto, PrizeColorModel>();
        CreateMap<PrizeColorModel, PrizeColorDto>();
    }
}