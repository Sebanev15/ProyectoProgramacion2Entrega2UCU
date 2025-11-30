// bot/botProject/src/Library/Interfaces/IModalHandler.cs
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Ucu.Poo.DiscordBot.Interfaces
{
    public interface IModalHandler
    {
        string CustomId { get; }
        Task HandleAsync(SocketModal modal);
    }
}