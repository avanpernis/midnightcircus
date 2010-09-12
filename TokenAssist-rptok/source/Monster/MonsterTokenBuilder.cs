using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class MonsterTokenBuilder
    {
        public static void WriteToken(Monster monster, string filename, string tokenImage, string tokenPortrait)
        {
            Token token = ActorTokenFactory.Create(monster, "4eMonster", tokenImage, tokenPortrait);

            // convert monster powers to token powers
            
            token.Write(filename);
        }
        
        public static TokenMacro MacroFromPower(MonsterPower power)
        {
            TokenMacro m = new TokenMacro();

            StringBuilder command = new StringBuilder();

            if (power.AttackBonus != null)
            {
            }

            return m;
        }
    }
}
