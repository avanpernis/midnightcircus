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
        public static string NoWeaponTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.NoWeaponTemplate;
            }
        }

        public static string WeaponTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.WeaponTemplate;
            }
        }

        public static void Dump(Character character, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Power power in character.Powers)
                {
                    if (power.Weapons.Count == 0)
                    {
                        // this power does not involve weapons -- use the appropriate template

                        string macro = NoWeaponTemplate;
                        macro = macro.Replace("__POWER_NAME__", power.Name);
                        macro = macro.Replace("__POWER_CARD__", (power.CompendiumEntry != null) ? power.CompendiumEntry : string.Empty);
                        writer.WriteLine(macro);
                    }
                    else
                    {
                        // this power involves weapons -- use the appropriate template

                        string macro = WeaponTemplate;
                        macro = macro.Replace("__POWER_NAME__", power.Name);
                        macro = macro.Replace("__ATTACK_BONUS__", power.Weapons[0].AttackBonus.ToString());
                        macro = macro.Replace("__DAMAGE__", power.Weapons[0].Damage);
                        macro = macro.Replace("__MAX_DAMAGE__", power.Weapons[0].MaxDamage);
                        macro = macro.Replace("__MULTIPLE_TARGETS__", "0");
                        macro = macro.Replace("__POWER_CARD__", (power.CompendiumEntry != null) ? power.CompendiumEntry : string.Empty);
                        writer.WriteLine(macro);
                    } 

                    writer.WriteLine("======================================================================");
                }
            }
        }
    }
}
