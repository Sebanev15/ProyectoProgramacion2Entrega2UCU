using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ucu.Poo.DiscordBot.Services
{
    public interface IBot
    {
        Task StartAsync(ServiceProvider services);
        
        Task StopAsync();
    }
}

