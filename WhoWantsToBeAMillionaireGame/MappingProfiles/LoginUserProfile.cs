using AutoMapper;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;
using WhoWantsToBeAMillionaireGame.Models.Admin;

namespace WhoWantsToBeAMillionaireGame.MappingProfiles
{
    public class LoginUserProfile:Profile
    {
        public LoginUserProfile()
        {
            CreateMap<LoginUserDto, LoginUser>();
            CreateMap<LoginUser, LoginUserDto>();
            CreateMap<LoginUserPostModel, LoginUserDto>();
            CreateMap<LoginUserDto, LoginUserPostModel>();
            CreateMap<LoginUserUpdateModel, LoginUserDto>();
            CreateMap<LoginUserDto, LoginUserUpdateModel>();
        }
    }
}
