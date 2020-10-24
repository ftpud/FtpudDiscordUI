using System;
using Discord;
namespace FtpudDiscordUI.Elements
{
    public class TextView : UiElement
    {
        public string Text { get; set; }

        public TextView(String text) => Text = text;

        public override void CreateElementView(EmbedBuilder builder)
        {
            builder.Description += Text + "\n";
        }
    }
}