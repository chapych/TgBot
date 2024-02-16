using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TgBot.Infrastructure.Services;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEventLoader, EventLoader>();
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
        
    }
}
