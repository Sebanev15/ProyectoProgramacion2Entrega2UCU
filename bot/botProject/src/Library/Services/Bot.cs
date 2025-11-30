using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ucu.Poo.DiscordBot.Services
{
    public class Bot : IBot
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _configuration;

        public Bot(DiscordSocketClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task StartAsync(ServiceProvider services)
        {
            string token = _configuration["DiscordToken"];
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Error: El token del bot no está configurado en los user secrets.");
                return;
            }

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }

        public async Task StopAsync()
        {
            await _client.LogoutAsync();
            await _client.StopAsync();
        }
    }
}
