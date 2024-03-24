using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Interface;
using SignalRChatApp.Models;
using SignalRChatApp.Repository;
using System;

namespace SignalRChatApp
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUser, UserRepo>();

            return services;
        }
    }
}
