using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TokenAssist
{
    public static class TokenGenerator
    {
        public static string TokenTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.template;
            }
        }

        public static void Dump(Character character, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Power power in character.Powers)
                {   
                    string macro = TokenTemplate;
                    macro = macro.Replace("POWER_NAME", power.Name);
                    macro = macro.Replace("ATTACK_BONUS", power.AttackBonus.ToString());
                    macro = macro.Replace("DAMAGE", power.Damage);
                    macro = macro.Replace("ATTACK_STAT", power.AttackStat.ToString());
                    macro = macro.Replace("DEFENSE", power.Defense.ToString());
                    macro = macro.Replace("MULTIPLE_TARGETS", "0");
                    macro = macro.Replace("POWER_CARD", (power.CompendiumEntry != null) ? power.CompendiumEntry : string.Empty);
                    writer.WriteLine(macro);

                    writer.WriteLine("======================================================================");
                }
            }
        }
    }
}
