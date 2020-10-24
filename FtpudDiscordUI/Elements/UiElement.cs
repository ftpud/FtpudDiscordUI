using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace FtpudDiscordUI.Elements
{
    public abstract class UiElement
    {
        public UiPage Parent { get; set; }

        public virtual void CreateElementView(EmbedBuilder builder) { }

        public virtual IEnumerable<IEmote> CreateElementReactions()
        {
            return new List<IEmote>();
        }

        public virtual Task HandleEvent(SocketReaction arg3)
        {
            return Task.CompletedTask;
        }
    }
}