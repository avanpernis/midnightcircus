using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TokenAssist
{
    public static class MonsterTokenBuilder
    {
        public static string gmStrWrapper(string input)
        {
            return "/gm <br><font size='4' color='blue'>{token.name}</font>\n" + input;
        }

        public static void WriteToken(Monster monster, string filename, string tokenImage, string tokenPortrait)
        {
            Token token = ActorTokenFactory.Create(monster, "4eMonster", tokenImage, tokenPortrait, gmStrWrapper);
            
            // when a monster is reset, it needs to know its starting amount of action points
            token.AddProperty("StartingActionPoints", monster.ActionPoints);

            // only monsters can be reset
            token.AddMacro(HtmlUtilities.Bold("Reset"), ActorTokenFactory.MiscGroup, Color.white, Color.black, Properties.Resources.ResetMonsterTemplate);

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
            command = command.Replace(@"###TYPE###", power.Category);
            command = command.Replace(@"###RANGE###", power.RangeText);

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

            if ((power.AttackBonus != null) && (power.Defense != null))
            {
                desc += string.Format("<i>Attack:</i> {0}; {1}{2} vs. {3}<br>", power.RangeText, (power.AttackBonus >= 0) ? "+" : "", power.AttackBonus, power.Defense);
            }
            if (power.OnHitText != null)
            {
                desc += "<i>Hit:</i> " + power.OnHitText + "<br>";
            }
            if (power.EffectText != null)
            {
                desc += "<i>Effect:</i> " + power.EffectText + "<br>";
            }

            command = command.Replace(@"###POWER_CARD###", desc);

            token.AddMacro("<b>"+power.Name+"</b><br>"+power.Action+" "+power.RangeText, power.Category, ColorFromCatagory(power.Category), Color.black, command);
        }

        public static ColorValue ColorFromCatagory(string Category)
        {
            ColorValue result = Color.white;

            if (Category.Equals("at-will", StringComparison.CurrentCultureIgnoreCase))
            {
                result = Color.green;
            }
            else if (Category.Equals("encounter", StringComparison.CurrentCultureIgnoreCase))
            {
                result = Color.red;
            }
            else if (Category.Equals("recharge", StringComparison.CurrentCultureIgnoreCase))
            {
                result = Color.red;
            }

            return result;
        }
    }
}
