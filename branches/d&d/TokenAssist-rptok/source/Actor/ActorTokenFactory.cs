using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class ActorTokenFactory
    {
        public static Token Create(Actor actor, string tokenType, string tokenImage, string tokenPortrait)
        {
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

            string groupName = "1:Check";

            string abilityChecks = Properties.Resources.CheckTemplate;
            abilityChecks = abilityChecks.Replace(@"__CHECK_NAME_LIST__", string.Join(",", actor.Abilities.Select(x => x.Key).ToArray()));
            abilityChecks = abilityChecks.Replace(@"__CHECK_BONUS_LIST__", string.Join(",", actor.Abilities.Select(x => x.Value.Modifier + actor.HalfLevel).ToArray()));
            token.AddMacro("Ability", groupName, ColorType.white, ColorType.black, abilityChecks);

            string skillChecks = Properties.Resources.CheckTemplate;
            skillChecks = skillChecks.Replace(@"__CHECK_NAME_LIST__", string.Join(",", actor.Skills.Select(x => x.Key).ToArray()));
            skillChecks = skillChecks.Replace(@"__CHECK_BONUS_LIST__", string.Join(",", actor.Skills.Select(x => x.Value).ToArray()));
            token.AddMacro("Skill", groupName, ColorType.white, ColorType.black, skillChecks);

            string savingThrow = Properties.Resources.SavingThrowTemplate;
            savingThrow = savingThrow.Replace(@"__SAVE_BONUS__", actor.SavingThrow.ToString());
            token.AddMacro("Saving Throw", groupName, ColorType.white, ColorType.black, savingThrow);

            groupName = "2:Misc";

            // TODO: healing, etc

            return token;
        }
    }
}
