﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class MonsterTokenBuilder
    {
        public static void WriteToken(Monster monster, string filename, string tokenImage, string tokenPortrait)
        {
            Token t = new Token();

            t.Name = monster.Name;
            t.TokenType = "4eMonster";
            t.TokenImage = tokenImage;
            t.TokenPortrait = tokenPortrait;

            t.AddProperty("Level", monster.Level);
            t.AddProperty("HalfLevel", monster.HalfLevel);
            t.AddProperty("Speed", monster.Speed);
            t.AddProperty("ActionPoints", 0); // TODO

            t.AddProperty("CurrentHitPoints", monster.HP);
            t.AddProperty("MaxHitPoints", monster.HP);
            t.AddProperty("TempHitPoints", 0);

            t.AddProperty("CurrentHealingSurges", 1); // TODO
            t.AddProperty("MaxHealingSurges", 1); // TODO
            t.AddProperty("HealingSurgeValue", 1337); // TODO

            foreach (KeyValuePair<string, AbilityScore> pair in monster.Abilities)
            {
                t.AddProperty(pair.Key, pair.Value.Value);
                t.AddProperty(pair.Key + "Modifier", pair.Value.Modifier);
            }

            foreach (KeyValuePair<string, int?> pair in monster.Defenses)
            {
                t.AddProperty(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, int> pair in monster.Skills)
            {
                t.AddProperty(pair.Key, pair.Value);
            }
            
            t.Write(filename);
        }
    }
}
