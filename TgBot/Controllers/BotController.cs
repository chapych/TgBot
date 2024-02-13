using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using UseCase.Interfaces;

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
    public async Task Post(Update update) 
    {
        await _bot.HandleUpdate(update);
    }
    [HttpGet]
    public string Get()
    {
        return "Telegram bot was started";
    }
}