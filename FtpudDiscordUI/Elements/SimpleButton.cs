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
            this._emote = emote;
            this.OnClick = onClick;
        }

        public override void CreateElementReactions(List<IEmote> emotes)
        {
            emotes.Add(new Emoji(_emote));
        }

        public override async Task HandleEvent(SocketReaction reaction)
        {
            if (reaction.Emote.Name == _emote)
            {
                await OnClick.Invoke();
            }
        }
    }
}