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
            Token t = ActorTokenFactory.Create(character, "4ePlayer", tokenImage, tokenPortrait);

            // TODO custom player stuff

            t.Write(filename);
        }
    }
}
