using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TokenAssist
{
    public class MonsterTokenBuilder : Builder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="filename"></param>
        /// <param name="tokenImage"></param>
        /// <param name="tokenPortrait"></param>
        public static void WriteToken(Monster monster, string filename, string tokenImage, string tokenPortrait)
        {
            MonsterTokenBuilder builder = new MonsterTokenBuilder(monster, tokenImage, tokenPortrait);
            Token token = builder.Token;
            token.Write(filename);
        }


        protected static string gmStrWrapper(string input)
        {
            return "/gm <br><font size='4' color='blue'>{token.name}</font>\n" + input;
        }


        public MonsterTokenBuilder(Monster monster, string tokenImage, string tokenPortrait)
        {
            mToken = ActorTokenFactory.Create(monster, "4eMonster", tokenImage, tokenPortrait, gmStrWrapper);

            // right now, only monsters support immunities, resistances, and vulnerabilities
            if (monster.Immunities.Count > 0)
            {
                mToken.AddProperty("Immune", string.Join(", ", monster.Immunities));
            }

            if (monster.Resistances.Count > 0)
            {
                mToken.AddProperty("Resist", string.Join(", ", monster.Resistances));
            }

            if (monster.Vulnerabilities.Count > 0)
            {
                mToken.AddProperty("Vulnerable", string.Join(", ", monster.Vulnerabilities));
            }

            // when a monster is reset, it needs to know its starting amount of action points
            mToken.AddProperty("StartingActionPoints", monster.ActionPoints);

            // only monsters can be reset
            mToken.AddMacro(HtmlUtilities.Bold("Reset"), ActorTokenFactory.MiscGroup, Color.white, Color.black, Properties.Resources.ResetMonsterTemplate);

            // add monster power macros
            foreach (MonsterPower p in monster.Powers)
            {
                BuildMacroFromPower(p);
            }

            // convert monster traits to token macros
            foreach (Trait trait in monster.Traits)
            {
                BuildMacroFromTrait(trait);
            }
        }


        public void BuildMacroFromPower(MonsterPower power)
        {
            string buttonTitle = HtmlUtilities.Bold(power.Name) + "<br>" + power.Action + " " + power.RangeText;

            string macroBody = MacroBodyFromPower(power);
            string macroText = macroBody;
            
            string group = power.Usage.ToString();

            if (power.isLimitedUse())
            {
                macroText = TokenAssist.Properties.Resources.LimitedUseTemplate;
                macroText = macroText.Replace(@"__POWER_ID__", mPowerId.ToString());
                macroText = macroText.Replace(@"__MACRO_TEXT__", macroBody);
                macroText = macroText.Replace(@"__USAGE_TYPE__", group);

                ++mPowerId;
            }

            // hide for gm
            macroText = gmStrWrapper(macroText);

            mToken.AddMacro(
                buttonTitle, group, GetMacroButtonColor(power), 
                GetMacroFontColor(power), macroText
            );
        }

        public static string MacroBodyFromPower(MonsterPower power)
        {
            string command = null;

            command = TokenAssist.Properties.Resources.MonsterPowerMacro;
            command = command.Replace(@"###NAME###", power.Name);
            command = command.Replace(@"###TYPE###", power.Usage.ToString());
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

            string targetsText = string.Empty;
            if (!string.IsNullOrWhiteSpace(power.Targets))
            {
                targetsText = string.Format(" ({0})", power.Targets);
            }

            if ((power.AttackBonus != null) && (power.Defense != null))
            {
                desc += string.Format("<i>Attack:</i> {0}{1}; {2}{3} vs. {4}<br>", power.RangeText, targetsText, (power.AttackBonus >= 0) ? "+" : "", power.AttackBonus, power.Defense);
            }

            if (power.OnHitText != null)
            {
                desc += "<i>Hit:</i> ";
                if (power.HitDamageExp != null)
                    desc += HtmlUtilities.Bold(power.HitDamageExp) + " ";
                desc += power.OnHitText + "<br>";
            }

            if (power.OnMissText != null)
            {
                desc += "<i>Miss:</i> ";
                if (power.MissDamageExp != null)
                    desc += HtmlUtilities.Bold(power.MissDamageExp) + " ";
                desc += power.OnMissText + "<br>";
            }

            if (power.EffectText != null)
            {
                desc += "<i>Effect:</i> " + power.EffectText + "<br>";
            }

            command = command.Replace(@"###POWER_CARD###", desc);

            return command;
        }

        public void BuildMacroFromTrait(Trait trait)
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

            mToken.AddMacro(name, "Traits", Color.MonsterCategory, Color.white, command);
        }

        /// <summary>
        ///  The Token object that this instance will build and act upon
        /// </summary>
        public Token Token
        {
            get { return mToken; }
        }
        private Token mToken = null;

        private int mPowerId = 0;
    }
}
