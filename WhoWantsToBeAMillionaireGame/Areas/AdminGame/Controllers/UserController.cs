using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WhoWantsToBeAMillionaireGame.Business.ServicesImplementations;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Models;
using WhoWantsToBeAMillionaireGame.Models.Admin;

namespace WhoWantsToBeAMillionaireGame.Areas.AdminGame.Controllers
{
    [Authorize]
    [Route("Admin/[controller]/[action]")]
    [Area("AdminGame")]
    public class UserController : Controller
    {
        public ILoginUserService _userService { get; }
        public IMapper Mapper { get; }

        public UserController(ILoginUserService userService, IMapper mapper)
        {
            _userService = userService;
            Mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] LoginUserPostModel data)
        {
            var allUsers = await _userService.GetAllUsersAsync();
            if (allUsers.Select(x => x.Email).Contains(data.Email)) return Json("already have");

            var dto = Mapper.Map<LoginUserDto>(data);
            var response = await _userService.CreateUserAsync(dto);
            return Json("success");
        }
        [HttpGet]
        public async Task<IActionResult> LoadGrid()
        {
            var allUsers = await _userService.GetAllUsersAsync();
            return Ok(allUsers);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Json("ok");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] LoginUserUpdateModel data)
        {
            var allUsers = await _userService.GetAllUsersAsync();
            if (data.Id != null)
            {
                var dto = Mapper.Map<LoginUserDto>(data);
                var response = await _userService.UpdateUserAsync(dto);
                return Json("ok");
            }
            return Json("notok");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            if (id != null)
            {
                var user = await _userService.GetUserById(id);
                return Json(user);
            }
            return Json("nonUser");
        }
    }
}
