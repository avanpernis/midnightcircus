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

                        string weaponList = string.Empty;
                        string attackBonusList = string.Empty;
                        string damageList = string.Empty;
                        string maxDamageList = string.Empty;

                        foreach (Weapon weapon in power.Weapons)
                        {
                            weaponList += (weaponList.Length == 0) ? weapon.Name : string.Format(", {0}", weapon.Name);
                            attackBonusList += (attackBonusList.Length == 0) ? weapon.AttackBonus.ToString() : string.Format(", {0}", weapon.AttackBonus);
                            damageList += (damageList.Length == 0) ? weapon.Damage : string.Format(", {0}", weapon.Damage);
                            maxDamageList += (maxDamageList.Length == 0) ? weapon.MaxDamage : string.Format(", {0}", weapon.MaxDamage);
                        }

                        string macro = WeaponTemplate;
                        macro = macro.Replace("__POWER_NAME__", power.Name);
                        macro = macro.Replace("__WEAPON_LIST__", weaponList);
                        macro = macro.Replace("__ATTACK_BONUS_LIST__", attackBonusList);
                        macro = macro.Replace("__DAMAGE_LIST__", damageList);
                        macro = macro.Replace("__MAX_DAMAGE_LIST__", maxDamageList);
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
