using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace FtpudDiscordUI.Elements
{
    public abstract class UiElement
    {
        public UiPage parent;

        public virtual void CreateElementView(EmbedBuilder builder) { }

        public virtual void CreateElementReactions(List<IEmote> list) { }
        
        public void SetParent(UiPage page)
        {
            parent = page;
        }

        public virtual Task HandleEvent(SocketReaction arg3)
        {
            return Task.CompletedTask;
        }
    }
}