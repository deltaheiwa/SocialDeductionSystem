using Discord.WebSocket;

namespace SocialDeductionSystem.DiscordBot.Interfaces;

public interface ICommandHandler<in T> where T : SocketEntity<ulong>
{
    Task HandleCommand(T socketInput);
}
