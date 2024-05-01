using Microsoft.Extensions.DependencyInjection;
using TgBot.Entities.Enums;
using TgBot.UseCase.Interfaces;
using TgBot.UseCase.Services;
using TgBot.UseCase.Services.Factories.StateFactory;
using TgBot.UseCase.Services.KeyboardFactory;

namespace TgBot.UseCase.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            services.AddScoped<IKeyboardFactory, KeyboardFactory>();
            services.AddScoped<IStateFactory, StateFactory>();
            services.AddScoped<IStateMachine, StateMachine>();

            return services;
        }
    }
}
