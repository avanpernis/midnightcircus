using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class TokenGenerator
    {
        public static void Dump(Character character, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Power power in character.Powers)
                {
                    writer.WriteLine(power.Name);
                    writer.WriteLine(power.Usage.ToString());
                    
                    if (power.AttackBonus != Power.DefaultAttackBonus)
                    {
                        writer.WriteLine("Attack = /roll d20 + " + power.AttackBonus);
                    }

                    if (power.Damage != Power.DefaultDamage)
                    {
                        writer.WriteLine("Damage = /roll " + power.Damage);
                    }

                    if (power.Url != null)
                    {
                        writer.WriteLine(power.Url);
                    }

                    if (power.CompendiumEntry != null)
                    {
                        writer.WriteLine(power.CompendiumEntry);
                    }

                    writer.WriteLine("======================================================================");
                }
            }
        }
    }
}
