using Discord.WebSocket;

namespace DiscordBot.Interfaces;

public interface ICommandHandler<in T> where T : SocketEntity<ulong>
{
    Task HandleCommand(T socketInput);
}
