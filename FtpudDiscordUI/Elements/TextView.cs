using System;
using Discord;
namespace FtpudDiscordUI.Elements
{
    public class TextView : UiElement
    {
        private volatile String _text;

        public string Text
        {
            get => _text;
            set => _text = value;
        }

        public TextView(String text)
        {
            _text = text;
        }

        public override void CreateElementView(EmbedBuilder builder)
        {
            builder.Description += Text + "\n";
        }
    }
}