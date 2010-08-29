﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class MonsterTokenBuilder
    {
        public static void WriteToken(Monster m, string filename)
        {
            Token t = new Token();

            foreach (KeyValuePair<string, AbilityScore> pair in m.Abilities)
            {
                TokenProperty p = new TokenProperty();
                p.Name = pair.Key;
                p.Value = pair.Value.Value.ToString();
                t.Properties.Add(p);
            }

            foreach (KeyValuePair<string, int?> pair in m.Defenses)
            {
                TokenProperty p = new TokenProperty();
                p.Name = pair.Key;
                p.Value = pair.Value.ToString();
                t.Properties.Add(p);
            }

            foreach (KeyValuePair<string, int> pair in m.Skills)
            {
                TokenProperty p = new TokenProperty();
                p.Name = pair.Key;
                p.Value = pair.Value.ToString();
                t.Properties.Add(p);
            }
            
            t.Write(filename);
        }
    }
}