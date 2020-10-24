using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace FtpudDiscordUI.Elements
{
    public class SimpleButton : UiElement
    {
        private readonly String _emote;
        public readonly Func<Task> OnClick;
        public SimpleButton(String emote, Func<Task> onClick)
        {
            _emote = emote;
            OnClick = onClick;
        }

        public override IEnumerable<IEmote> CreateElementReactions()
        {
            return new List<IEmote> { new Emoji(_emote) };
        }

        public override async Task HandleEvent(SocketReaction reaction)
        {
            if (reaction.Emote.Name == _emote)
            {
                await OnClick();
            }
        }
    }
}