using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TgBot.UseCase.Interfaces;
using TgBot.UseCase.Services.Bot;
using TgBot.UseCase.Services.KeyboardFactory;

namespace TgBot.UseCase.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            services.AddScoped<IBot, Bot>();
            services.AddScoped<IKeyboardFactory, KeyboardFactory>();

            return services;
        }
    }
}
