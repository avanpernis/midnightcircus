using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class CharacterTokenBuilder
    {
        public static void WriteToken(Character c, string filename, string tokenImage, string tokenPortrait)
        {
            Token t = new Token();

            t.TokenImage = tokenImage;
            t.TokenPortrait = tokenPortrait;
            
            t.Write(filename);
        }
    }
}
