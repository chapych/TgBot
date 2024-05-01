using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TgBot.UseCase.Interfaces;

namespace TgBot.Controllers;

[ApiController]
[Route("/")]
public class BotController : Controller
{
    private readonly IBot _bot;

    public BotController(IBot bot)
    {
        _bot = bot;
    }

    [HttpPost]
    public Task Post(Update update) 
    {
        return _bot.HandleUpdate(update);
    }
    [HttpGet]
    public string Get()
    {
        return "Telegram bot was started";
    }
}