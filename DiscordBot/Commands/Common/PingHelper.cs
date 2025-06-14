using Discord.WebSocket;
using Serilog;

namespace SocialDeductionSystem.DiscordBot.Commands.Common;

public static class PingHelper
{
    public static string GetPingMessage(DiscordSocketClient client)
    {
        Log.Debug("Current latency: {Latency}ms", client.Latency);
        return $"Pong {client.Latency}ms";
    }
}