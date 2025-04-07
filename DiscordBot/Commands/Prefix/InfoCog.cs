using Discord.Commands;

namespace DiscordBot.Commands.Prefix;

public class InfoCog : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Summary("Replies with Pong!")]
    public async Task PingAsync()
    {
        await ReplyAsync($"Pong {Context.Client.Latency}ms");
    }
}