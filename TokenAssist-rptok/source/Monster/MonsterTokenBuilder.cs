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
            foreach (MonsterPower p in monster.Powers)
            {
                BuildMacroFromPower(token, p);
            }
            
            token.Write(filename);
        }

        public static void BuildMacroFromPower(Token token, MonsterPower power)
        {
            string command = null;

            if ((power.AttackBonus != null) && (power.Damage != null))
            {
                command = TokenAssist.Properties.Resources.MonsterPowerMacro;
                command = command.Replace(@"###NAME###", power.Name);
                command = command.Replace(@"###ATTACK_BONUS###", power.AttackBonus.ToString());
                command = command.Replace(@"###DAMAGE###", power.Damage);
                command = command.Replace(@"###MAX_DAMAGE###", RollUtilities.EvaluateMaximum(power.Damage).ToString());
                command = command.Replace(@"###DEFENSE_STAT###", power.Defense);

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

            token.AddMacro(power.Name, power.Category, ColorType.white, ColorType.black, command);
        }
    }
}
