using System;
using Discord;

namespace FtpudDiscordUI.Elements
{
    public class CheckBox : TextView
    {

        public bool Checked { get; set; } = false;
        
        public CheckBox(String text) : base(text)
        { }

        public override void CreateElementView(EmbedBuilder builder)
        {
            builder.Description += (Checked?"\u2705 ":"\u274E ") + Text + "\n";
        }
    }
}