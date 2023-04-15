using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Models.Admin.AdvertiseModels;

namespace WhoWantsToBeAMillionaireGame.Areas.AdminGame.Controllers
{
    [Authorize]
    [Area("AdminGame")]
    public class AdvertisingController : Controller
    {
        public IAdvertiseService _advertiseService { get; }
        private readonly IWebHostEnvironment _environment;
        private readonly ISocialMediaLinkService _socialMediaLinkService;
        public IMapper Mapper { get; }

        public AdvertisingController(IAdvertiseService advertiseService, IMapper mapper, IWebHostEnvironment environment, ISocialMediaLinkService socialMediaLinkService)
        {
            _advertiseService = advertiseService;
            Mapper = mapper;
            _environment = environment;
            _socialMediaLinkService = socialMediaLinkService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LoadGridData()
        {
            var advertises = await _advertiseService.GetAllAdvertisesAsync();
            return Json(advertises.Where(x => x.IsActive == true));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdvertise(IFormFile file, [FromForm] AdvertisePostModel data)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is not selected or empty.");
                }

                if (!file.ContentType.StartsWith("image/"))
                {
                    return BadRequest("Invalid file type.");
                }

                string uploadsFolder = Path.Combine("uploads");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var dto = Mapper.Map<AdvertiseDto>(data);
                dto.Id = Guid.NewGuid();
                dto.ImageUrl = Path.Combine("uploads", uniqueFileName);
                dto.AdType = string.Empty;
                dto.AdSize = string.Empty;
                dto.CreatedDate = DateTime.UtcNow;
                dto.ModifiedDate = DateTime.MinValue;
                dto.AdPlacement = string.Empty;
                dto.IsActive = true;
                dto.AdStatus = false;

                var response = await _advertiseService.CreateAdvertiseAsync(dto);
                return Json("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActivateAdvertise([FromQuery] Guid Id)
        {
            var allAdvertises = await _advertiseService.GetAllAdvertisesAsync();
            if (allAdvertises.Any(x => x.AdStatus == true)) return Json("alreadyActive");
            var advertise = await _advertiseService.GetAdvertiseByIdAsync(Id);
            advertise.AdStatus = true;
            await _advertiseService.UpdateAdvertiseAsync(advertise);
            return Json("success");
        }
        [HttpPost]
        public async Task<IActionResult> DeactiveAdvertise([FromQuery] Guid Id)
        {
            var advertise = await _advertiseService.GetAdvertiseByIdAsync(Id);
            if (advertise.AdStatus == true)
            {
                advertise.AdStatus = false;
                await _advertiseService.UpdateAdvertiseAsync(advertise);
                return Json("success");
            }
            return Json("already");

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAdvertise([FromQuery] Guid Id)
        {
            var advertise = await _advertiseService.GetAdvertiseByIdAsync(Id);
            if (advertise != null)
            {
                await _advertiseService.DeleteAdvertiseAsync(Id);
                return Json("success");
            }
            return Json("notFound");
        }
        [HttpGet]
        public async Task<IActionResult> GetAdvertisementDetails([FromQuery] Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var advertise = await _advertiseService.GetAdvertiseByIdAsync(Id);
                return Json(advertise);
            }

            return Json("notFound");
        }

        [HttpPost]
        public async Task<IActionResult> SaveSocialMediaLinks([FromForm] SocialMediaLinkDto socialMediaLinkDto)
        {
            if (socialMediaLinkDto != null)
            {
                var entity = await _socialMediaLinkService.GetSocialMediaLink();
                entity.FacebookUrl = socialMediaLinkDto.FacebookUrl;
                entity.TikTokUrl = socialMediaLinkDto.TikTokUrl;
                entity.InstagramUrl = socialMediaLinkDto.InstagramUrl;
                await _socialMediaLinkService.UpdateSocialMediaLink(entity);
                return Json("OK");
            }
            return Json("notFound");
        }

    }
}
