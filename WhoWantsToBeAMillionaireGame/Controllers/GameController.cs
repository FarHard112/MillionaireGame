﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Models;
using WhoWantsToBeAMillionaireGame.SessionUtils;

namespace WhoWantsToBeAMillionaireGame.Controllers;

public class GameController : Controller
{
    private readonly IMapper _mapper;
    private readonly IGameService _gameService;
    private readonly IPrizeService _prizeService;
    private readonly IAdvertiseService _advertiseService;
    private readonly IClickedAdService _clickedAdService;
    private readonly IGameTimer _gameTimerService;
    private readonly ISocialMediaLinkService _socialMediaLinkService;
    private const string GameSessionKey = "_Game";

    public GameController(IMapper mapper,
        IGameService gameService, IPrizeService prizeService, IAdvertiseService advertiseService, IClickedAdService clickedAdService, IGameTimer gameTimerService, ISocialMediaLinkService socialMediaLinkService)
    {
        _mapper = mapper;
        _gameService = gameService;
        _prizeService = prizeService;
        _advertiseService = advertiseService;
        _clickedAdService = clickedAdService;
        _gameTimerService = gameTimerService;
        _socialMediaLinkService = socialMediaLinkService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);
            if (!isSucceed)
            {
                gameSession = new GameSession
                {
                    GameId = Guid.NewGuid(),
                    UserChoiceId = Guid.Empty,
                    QuestionNumber = 1,
                    IsTookMoney = false
                };
                HttpContext.Session.Set(GameSessionKey, gameSession);
                await _gameService.CreateNewGameAsync(gameSession.GameId);
            }
            var dto = await _gameService.GetGameById(gameSession.GameId);
            var model = _mapper.Map<GameModel>(dto);
            model.PrizeList = await _prizeService.GetAllPrizesAsync();
            var advertises = await _advertiseService.GetAllAdvertisesAsync();
            if (advertises.FirstOrDefault(x => x.AdStatus) != null) model.Advertisement = advertises.FirstOrDefault(x => x.AdStatus);
            model.UserChoice = gameSession.UserChoiceId;
            model.gameTimer = await _gameTimerService.GetGameTimer();
            var entity = await _socialMediaLinkService.GetSocialMediaLink();
            ViewBag.TikTokUrl = entity.TikTokUrl;
            ViewBag.InstagramUrl = entity.InstagramUrl;
            ViewBag.FacebookUrl = entity.FacebookUrl;
            return View(model);
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> IsUserChoiceCorrect([FromBody] Guid userChoice)
    {
        try
        {
            var gameSession = HttpContext.Session.Get<GameSession>(GameSessionKey);
            if (gameSession != null)
            {
                gameSession.UserChoiceId = userChoice;
                HttpContext.Session.Set(GameSessionKey, gameSession);
            }

            var isCorrect = await _gameService.IsAnswerCorrect(userChoice);

            return Ok(isCorrect);
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult GetCorrectAnswerId()
    {
        try
        {
            var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);

            //todo: add a middle page with information about the end of the waiting time
            if (!isSucceed)
                RedirectToAction("Index", "Home");

            var correctAnswerId = _gameService.GetIdForCorrectAnswerOfCurrentQuestionByGameIdAsync(gameSession.GameId);
            return Ok(correctAnswerId);
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult GameOver()
    {
        try
        {
            HttpContext.Session.Remove(GameSessionKey);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> MarkGameCurrentQuestionAsSuccessful()
    {
        try
        {
            var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);

            //todo: add a middle page with information about the end of the waiting time
            if (!isSucceed)
                RedirectToAction("Index", "Home");

            var result = await _gameService.MarkCurrentGameQuestionAsSuccessful(gameSession.GameId);
            return Ok(result != 0);
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult GetCurrentQuestionNumber()
    {
        try
        {
            var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);

            //todo: add a middle page with information about the end of the waiting time
            if (!isSucceed)
                RedirectToAction("Index", "Home");

            return Ok(gameSession.QuestionNumber);
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult TookMoney()
    {
        try
        {
            var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);

            //todo: add a middle page with information about the end of the waiting time
            if (!isSucceed)
                RedirectToAction("Index", "Home");

            gameSession.IsTookMoney = true;

            HttpContext.Session.Set(GameSessionKey, gameSession);

            return Ok(true);
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult IsUserTookMoney()
    {
        try
        {
            var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);

            //todo: add a middle page with information about the end of the waiting time
            if (!isSucceed)
                RedirectToAction("Index", "Home");

            return Ok(gameSession.IsTookMoney);
        }
        catch (Exception e)
        {
            Log.Error($"{e.Message}. {Environment.NewLine} {e.StackTrace}");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult GetNextGameQuestion()
    {
        try
        {
            var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);

            //todo: add a middle page with information about the end of the waiting time
            if (!isSucceed)
                RedirectToAction("Index", "Home");

            var gameQuestion = _gameService.GetCurrentQuestionByGameIdAsync(gameSession.GameId);

            gameSession.UserChoiceId = Guid.Empty;
            gameSession.QuestionNumber++;

            HttpContext.Session.Set(GameSessionKey, gameSession);

            return Ok(gameQuestion);
        }
        catch (ArgumentException ex)
        {
            var questionNumber = GetQuestionNumberFromSession();
            if (questionNumber.Equals(15)) return Ok();

            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            return StatusCode(500);
        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    private int GetQuestionNumberFromSession()
    {
        var isSucceed = HttpContext.Session.TryGetValue<GameSession>(GameSessionKey, out var gameSession);

        //todo: add a middle page with information about the end of the waiting time
        if (!isSucceed)
            throw new ArgumentException("Failed to get value from session find session.");

        return gameSession.QuestionNumber;
    }
    //Count user advertise Click 
    [HttpPost]
    public async Task<IActionResult> RegisterAdClick([FromQuery] Guid Id)
    {
        try
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _clickedAdService.RegisterAdClick(Id, ipAddress);
            return Json("OK");
        }
        catch (Exception ex)
        {

            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            return StatusCode(500);
        }


    }
}