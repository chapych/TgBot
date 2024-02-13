using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TgBot.Models.EF;
using UseCase.Interfaces;
using UseCase.Services.Bot;
using UseCase.Services.KeyboardFactory;
using UseCase.Settings;
using TypeConverter = System.ComponentModel.TypeConverter;

namespace TgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson();
        var telegramSettings = builder.Configuration.GetSection(nameof(TelegramSettings));
        builder.Services.Configure<TelegramSettings>(telegramSettings);
        //var kudaGoSettings = builder.Configuration.GetSection(nameof(KudaGoSettings));
        //builder.Services.Configure<KudaGoSettings>(kudaGoSettings);
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

        builder.Services.AddScoped<IBot, Bot>();
        builder.Services.AddScoped<IKeyboardFactory, KeyboardFactory>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}