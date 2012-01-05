using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class ActorTokenFactory
    {
        public static string CheckGroup = "1:Check";
        public static string MiscGroup = "2:Misc";

        private static string noChangeStrWrapper(string input)
        {
            return input;
        }

        public static Token Create(Actor actor, string tokenType, string tokenImage, string tokenPortrait, System.Func<string, string> strWrapper = null)
        {
            if (strWrapper == null)
                strWrapper = noChangeStrWrapper;

            Token token = new Token();

            token.Name = actor.Name;
            token.TokenType = tokenType;
            token.TokenImage = tokenImage;
            token.TokenPortrait = tokenPortrait;

            token.AddProperty("Level", actor.Level);
            token.AddProperty("HalfLevel", actor.HalfLevel);
            token.AddProperty("Speed", actor.Speed);
            token.AddProperty("ActionPoints", actor.ActionPoints);

            token.AddProperty("CurrentHitPoints", actor.HP);
            token.AddProperty("MaxHitPoints", actor.HP);
            token.AddProperty("TempHitPoints", 0);

            token.AddProperty("CurrentHealingSurges", actor.HealingSurges);
            token.AddProperty("MaxHealingSurges", actor.HealingSurges);
            token.AddProperty("HealingSurgeValue", actor.HealingSurgeValue);

            foreach (KeyValuePair<string, AbilityScore> pair in actor.Abilities)
            {
                token.AddProperty(pair.Key, pair.Value.Value);
                token.AddProperty(pair.Key + "Modifier", pair.Value.Modifier);
            }

            foreach (KeyValuePair<string, int?> pair in actor.Defenses)
            {
                token.AddProperty(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, int> pair in actor.Skills)
            {
                token.AddProperty(pair.Key, pair.Value);
            }           

            string abilityChecks = strWrapper(Properties.Resources.CheckTemplate);
            abilityChecks = abilityChecks.Replace(@"__CHECK_NAME_LIST__", string.Join(",", actor.Abilities.Select(x => x.Key).ToArray()));
            abilityChecks = abilityChecks.Replace(@"__CHECK_BONUS_LIST__", string.Join(",", actor.Abilities.Select(x => x.Value.Modifier + actor.HalfLevel).ToArray()));
            token.AddMacro(HtmlUtilities.Bold("Ability"), CheckGroup, Color.white, Color.black, abilityChecks);

            string skillChecks = strWrapper(Properties.Resources.CheckTemplate);
            skillChecks = skillChecks.Replace(@"__CHECK_NAME_LIST__", string.Join(",", actor.Skills.Select(x => x.Key).ToArray()));
            skillChecks = skillChecks.Replace(@"__CHECK_BONUS_LIST__", string.Join(",", actor.Skills.Select(x => x.Value).ToArray()));
            token.AddMacro(HtmlUtilities.Bold("Skill"), CheckGroup, Color.white, Color.black, skillChecks);

            string savingThrow = strWrapper(Properties.Resources.SavingThrowTemplate);
            savingThrow = savingThrow.Replace(@"__SAVE_BONUS__", actor.SavingThrow.ToString());
            token.AddMacro(HtmlUtilities.Bold("Saving Throw"), CheckGroup, Color.white, Color.black, savingThrow);
            
            // initiative should always be public or the tool shall choke
            string initiative = Properties.Resources.InitiativeTemplate;
            initiative = initiative.Replace(@"__INIT_BONUS__", actor.Initiative.ToString());
            token.AddMacro(HtmlUtilities.Bold("Initiative"), CheckGroup, Color.white, Color.black, initiative);

            token.AddMacro(HtmlUtilities.Bold("Healing"), MiscGroup, Color.white, Color.black, strWrapper(Properties.Resources.HealingTemplate));
            token.AddMacro(HtmlUtilities.Bold("Damage"), MiscGroup, Color.white, Color.black, strWrapper(Properties.Resources.DamageTemplate));
            token.AddMacro(HtmlUtilities.Bold("Temp HP"), MiscGroup, Color.white, Color.black, strWrapper(Properties.Resources.TempHPTemplate));
            token.AddMacro(HtmlUtilities.Bold("Action Point"), MiscGroup, Color.white, Color.black, strWrapper(Properties.Resources.ActionPointTemplate));

            return token;
        }
    }
}
