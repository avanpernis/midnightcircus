using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace TokenAssist
{
    public static class MonsterTokenBuilder
    {
        public static void WriteToken(Monster monster, string filename, string tokenImage, string tokenPortrait)
        {
            Token token = ActorTokenFactory.Create(monster, "4eMonster", tokenImage, tokenPortrait);

            // convert monster powers to token powers
            foreach (MonsterPower p in monster.Powers)
            {
                TokenMacro m = MacroFromPower(p);
                token.AddMacro(m);
            }
            
            token.Write(filename);
        }
        
        public static TokenMacro MacroFromPower(MonsterPower power)
        {
            TokenMacro macro = new TokenMacro();

            string command = null;

            macro.Name = SecurityElement.Escape(power.Name);
            macro.Group = SecurityElement.Escape(power.Category);

            macro.FontColor = ColorType.black;
            macro.ButtonColor = ColorType.white;

            if ((power.AttackBonus != null) && (power.Damage != null))
            {
                command = TokenAssist.Properties.Resources.MonsterPowerMacro;
                command = command.Replace(@"###NAME###", power.Name);
                command = command.Replace(@"###ATTACK_BONUS###", power.AttackBonus.ToString());
                command = command.Replace(@"###DAMAGE###", power.Damage);
                command = command.Replace(@"###MAX_DAMAGE###", RollUtilities.EvaluateMaximum(power.Damage).ToString());

                if (power.MultiTarget == true)
                    command = command.Replace(@"###MULTIPLE_TARGETS###", "1");
                else
                    command = command.Replace(@"###MULTIPLE_TARGETS###", "0");
            }

            // load the description
            string desc = "";
            if (power.Description != null)
                desc = power.Description;

            command = command.Replace(@"###POWER_CARD###", desc);

            macro.Command = SecurityElement.Escape(command);

            return macro;
        }
    }
}
