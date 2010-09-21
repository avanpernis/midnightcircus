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

            // when a monster is reset, it needs to know its starting amount of action points
            token.AddProperty("StartingActionPoints", monster.ActionPoints);

            // only monsters can be reset
            token.AddMacro(HtmlUtilities.Bold("Reset"), ActorTokenFactory.MiscGroup, ColorType.white, ColorType.black, Properties.Resources.ResetMonsterTemplate);

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

            command = TokenAssist.Properties.Resources.MonsterPowerMacro;
            command = command.Replace(@"###NAME###", power.Name);

            int attackBonus = (power.AttackBonus == null) ? 0 : (int)power.AttackBonus;
            command = command.Replace(@"###ATTACK_BONUS###", attackBonus.ToString());

            string dmg = (power.Damage == null) ? "0" : power.Damage;
            command = command.Replace(@"###DAMAGE###", power.Damage);
            try
            {
                string maxdmg = RollUtilities.EvaluateMaximum(power.Damage).ToString();
                command = command.Replace(@"###MAX_DAMAGE###", maxdmg);
            }
            catch
            {
                command = command.Replace(@"###MAX_DAMAGE###", "0");
            }
            

            command = command.Replace(@"###DEFENSE_STAT###", power.Defense);

            if (power.MultiTarget == true)
                command = command.Replace(@"###MULTIPLE_TARGETS###", "1");
            else
                command = command.Replace(@"###MULTIPLE_TARGETS###", "0");

            // load the description
            string desc = "";
            if (power.OnHitText != null)
            {
                desc += "Hit: " + power.OnHitText;
            }
            if (power.EffectText != null)
            {
                desc += "Effect: " + power.EffectText;
            }

            command = command.Replace(@"###POWER_CARD###", desc);

            token.AddMacro(power.Name, power.Category, ColorType.white, ColorType.black, command);
        }
    }
}
