using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class CharacterTokenBuilder
    {
        public static void WriteToken(Character character, string filename, string tokenImage, string tokenPortrait)
        {
            Token token = ActorTokenFactory.Create(character, "4ePlayer", tokenImage, tokenPortrait);

            // only characters take rests and finish milestones
            token.AddMacro(HtmlUtilities.Bold("Rest"), ActorTokenFactory.MiscGroup, ColorType.white, ColorType.black, Properties.Resources.RestTemplate);
            token.AddMacro(HtmlUtilities.Bold("Milestone"), ActorTokenFactory.MiscGroup, ColorType.white, ColorType.black, Properties.Resources.MilestoneTemplate);

            token.Write(filename);
        }
    }
}
