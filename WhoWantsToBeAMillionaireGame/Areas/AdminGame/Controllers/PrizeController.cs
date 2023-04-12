using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WhoWantsToBeAMillionaireGame.Business.ServicesImplementations;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Models;

namespace WhoWantsToBeAMillionaireGame.Controllers
{
    [Authorize]
    [Area("AdminGame")]
    public class PrizeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPrizeService _prizeService;
        private readonly IColorPrizeService _colorPrizeService;

        public PrizeController(IMapper mapper, IPrizeService prizeService, IColorPrizeService colorPrizeService)
        {
            _mapper = mapper;
            _prizeService = prizeService;
            _colorPrizeService = colorPrizeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var dto = await _prizeService.GetAllPrizesAsync();
                var model = _mapper.Map<List<PrizeModel>>(dto);
                return View(model);
            }
            catch (Exception ex)
            {

                Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
                return StatusCode(500);
            }

        }
        //todo : Heddiye page-i UI Form olan hisse
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var colorPrizes = await _colorPrizeService.GetAllColorsAsync();
            ViewBag.PrizesColor = colorPrizes;


            return View();
        }
        //todo: Heddiyeleri elave etmek 
        [HttpPost]
        public async Task<IActionResult> Create(PrizePostModel prizePostModel)
        {
            try
            {
                var dto = _mapper.Map<PrizeDto>(prizePostModel);
                await _prizeService.CreatePrizeAsync(dto);
                return RedirectToAction("Index", "Prize");
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException(nameof(id));

                await _prizeService.DeleteAsync(id);

                return RedirectToAction("Index", "Prize");
            }
            catch (ArgumentException ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
                return StatusCode(500);
            }

        }

    }
}
