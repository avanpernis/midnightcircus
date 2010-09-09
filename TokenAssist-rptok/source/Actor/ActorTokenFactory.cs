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

            return token;
        }
    }
}
