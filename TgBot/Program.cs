using KudaGo.Infrastructure.Configurations;
using KudaGo.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TgBot.Infrastructure.Context;
using TgBot.Infrastructure.Extensions;
using TgBot.UseCase.Extensions;
using TgBot.UseCase.Settings;

namespace TgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddNewtonsoftJson();

        var telegramSettings = builder.Configuration.GetSection(nameof(TelegramSettings));
        builder.Services.Configure<TelegramSettings>(telegramSettings);

        var kudaGoSettings = builder.Configuration.GetSection(nameof(KudaGoSettings));
        builder.Services.Configure<KudaGoSettings>(kudaGoSettings);

        var telegramBotClientSettings = builder.Configuration.GetSection(nameof(TelegramBotClientSettings));
        builder.Services.Configure<TelegramBotClientSettings>(telegramBotClientSettings);

        builder.Services.AddHttpClient<ITelegramBotClient, TelegramBotClient>(httpClient =>
        {
            var botConfig = telegramSettings.Get<TelegramSettings>();
            return botConfig == null
                ? throw new NullReferenceException()
                : new TelegramBotClient(new TelegramBotClientOptions(botConfig.Token!),
                    httpClient);
        });

        var connection = builder.Configuration.GetConnectionString("ApplicationContext");
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

        builder.Services.AddUseCase();
        builder.Services.AddInfrastructure();
        builder.Services.AddKudaGo();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}