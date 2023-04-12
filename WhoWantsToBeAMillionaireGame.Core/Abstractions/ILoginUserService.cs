using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

namespace WhoWantsToBeAMillionaireGame.Core.Abstractions
{
    public interface ILoginUserService
    {
        Task<List<LoginUserDto>>GetAllUsersAsync();
        Task<int> CreateUserAsync(LoginUserDto userDto);
        Task<int> DeleteUserAsync(Guid id);
        Task<int> UpdateUserAsync(LoginUserDto userDto);
        Task<LoginUserDto> GetUserById(Guid Id);
    }
}
