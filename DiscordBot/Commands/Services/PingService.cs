using Discord.WebSocket;
using Serilog;

namespace SocialDeductionSystem.DiscordBot.Commands.Services;

public static class PingService
{
    public static string GetPingMessage(DiscordSocketClient client)
    {
        Log.Debug("Current latency: {Latency}ms", client.Latency);
        return $"Pong {client.Latency}ms";
    }
}