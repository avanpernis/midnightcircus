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
                    macro = macro.Replace("__POWER_NAME__", power.Name);
                    macro = macro.Replace("__ATTACK_BONUS__", power.AttackBonus.ToString());
                    macro = macro.Replace("__DAMAGE__", power.Damage);
                    macro = macro.Replace("__MAX_DAMAGE__", power.MaxDamage);
                    macro = macro.Replace("__ATTACK_STAT__", power.AttackStat.ToString());
                    macro = macro.Replace("__DEFENSE__", power.Defense.ToString());
                    macro = macro.Replace("__MULTIPLE_TARGETS__", "0");
                    macro = macro.Replace("__POWER_CARD__", (power.CompendiumEntry != null) ? power.CompendiumEntry : string.Empty);
                    writer.WriteLine(macro);

                    writer.WriteLine("======================================================================");
                }
            }
        }
    }
}
