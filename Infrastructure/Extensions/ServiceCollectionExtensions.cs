using Microsoft.Extensions.DependencyInjection;
using TgBot.Infrastructure.Services;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBot, Bot>();
            services.AddScoped<IEventLoader, EventLoader>();
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            return services;
        }
        
    }
}
