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

            // right now, only monsters support immunities, resistances, and vulnerabilities
            if (monster.Immunities.Count > 0)
            {
                token.AddProperty("Immune", string.Join(", ", monster.Immunities));
            }

            if (monster.Resistances.Count > 0)
            {
                token.AddProperty("Resist", string.Join(", ", monster.Resistances));
            }

            if (monster.Vulnerabilities.Count > 0)
            {
                token.AddProperty("Vulnerable", string.Join(", ", monster.Vulnerabilities));
            }
            
            // when a monster is reset, it needs to know its starting amount of action points
            token.AddProperty("StartingActionPoints", monster.ActionPoints);

            // only monsters can be reset
            token.AddMacro(HtmlUtilities.Bold("Reset"), ActorTokenFactory.MiscGroup, Color.white, Color.black, Properties.Resources.ResetMonsterTemplate);

            // convert monster powers to token macros
            foreach (MonsterPower p in monster.Powers)
            {
                BuildMacroFromPower(token, p);
            }

            // convert monster traits to token macros
            foreach (Trait trait in monster.Traits)
            {
                BuildMacroFromTrait(token, trait);
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

            string dmg = (power.HitDamageExp == null) ? "0" : power.HitDamageExp;
            command = command.Replace(@"###DAMAGE###", power.HitDamageExp);
            try
            {
                string maxdmg = RollUtilities.EvaluateMaximum(power.HitDamageExp).ToString();
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

            if (power.TriggerText != null)
            {
                desc += "<i>Trigger:</i> " + power.TriggerText + "<br>"; ;
            }

            if ((power.AttackBonus != null) && (power.Defense != null))
            {
                desc += string.Format("<i>Attack:</i> {0}; {1}{2} vs. {3}<br>", power.RangeText, (power.AttackBonus >= 0) ? "+" : "", power.AttackBonus, power.Defense);
            }

            if (power.OnHitText != null)
            {
                desc += "<i>Hit:</i> ";
                if (power.HitDamageExp != null)
                    desc += "<b>" + power.HitDamageExp + "</b> ";
                desc += power.OnHitText + "<br>";
            }

            if (power.OnMissText != null)
            {
                desc += "<i>Miss:</i> ";
                if (power.MissDamageExp != null)
                    desc += "<b>" + power.MissDamageExp + "</b> ";
                desc += power.OnMissText + "<br>";
            }

            if (power.EffectText != null)
            {
                desc += "<i>Effect:</i> " + power.EffectText + "<br>";
            }

            command = command.Replace(@"###POWER_CARD###", desc);

            token.AddMacro(HtmlUtilities.Bold(power.Name)+"<br>"+power.Action+" "+power.RangeText, power.Category, ColorFromCategory(power.Category), Color.black, command);
        }

        public static void BuildMacroFromTrait(Token token, Trait trait)
        {
            string command = TokenAssist.Properties.Resources.MonsterTraitTemplate;
            command = command.Replace(@"__MONSTER_TRAIT_NAME__", trait.Name);
            command = command.Replace(@"__MONSTER_TRAIT_RANGE__", (trait.Range > 0) ? "Aura " + trait.Range : string.Empty);
            command = command.Replace(@"__MONSTER_TRAIT_KEYWORDS__", string.Join(", ", trait.Keywords));
            command = command.Replace(@"__MONSTER_TRAIT_DESCRIPTION__", HtmlUtilities.ScrubString(trait.Description));

            string name = HtmlUtilities.Bold(trait.Name);
            if (trait.Range > 0)
            {
                name += "<br>Aura " + trait.Range;
            }

            token.AddMacro(name, "Traits", Color.MonsterCategory, Color.white, command);
        }

        public static ColorValue ColorFromCategory(string Category)
        {
            ColorValue result = Color.white;

            if (Category != null)
            {
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
            }

            return result;
        }
    }
}
