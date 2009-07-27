using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace TokenAssist
{
    public static class TokenGenerator
    {
        public static string MacroCreationTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.MacroCreationTemplate;
            }
        }

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

        private static string GetMacroName(Power power)
        {
            return string.Format(@"<b>{0}</b><br>{1} {2}", power.Name, power.Action.ToString(), power.AttackTypeAndRange);
        }

        private static string GetMacroBackgroundColor(Power.UsageType usageType)
        {
            switch (usageType)
            {
                case Power.UsageType.AtWill:
                    return "green";
                case Power.UsageType.Encounter:
                    return "red";
                case Power.UsageType.Daily:
                    return "black";
                default:
                    return null;
            }
        }

        private static string GetMacroForegroundColor(Power.UsageType usageType)
        {
            switch (usageType)
            {
                case Power.UsageType.AtWill:
                    return "black";
                case Power.UsageType.Encounter:
                case Power.UsageType.Daily:
                    return "white";
                default:
                    return null;
            }
        }

        private static string GetMacroGroup(Power.UsageType usageType)
        {
            switch (usageType)
            {
                case Power.UsageType.AtWill:
                    return "At-Will";
                case Power.UsageType.Encounter:
                    return "Encounter";
                case Power.UsageType.Daily:
                    return "Daily";
                default:
                    return null;
            }
        }

        public static void Dump(Character character, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Power power in character.Powers)
                {
                    string macro = null;

                    if (power.Weapons.Count == 0)
                    {
                        // this power does not involve weapons -- use the appropriate template

                        macro = NoWeaponTemplate;
                        macro = macro.Replace(@"__POWER_NAME__", power.Name);
                        macro = macro.Replace(@"__POWER_CARD__", (power.CompendiumEntry != null) ? power.CompendiumEntry : string.Empty);
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

                        macro = WeaponTemplate;
                        macro = macro.Replace(@"__POWER_NAME__", power.Name);
                        macro = macro.Replace(@"__WEAPON_LIST__", weaponList);
                        macro = macro.Replace(@"__ATTACK_BONUS_LIST__", attackBonusList);
                        macro = macro.Replace(@"__DAMAGE_LIST__", damageList);
                        macro = macro.Replace(@"__MAX_DAMAGE_LIST__", maxDamageList);
                        macro = macro.Replace(@"__MULTIPLE_TARGETS__", power.AllowsForMultipleAttacks ? "1" : "0");
                        macro = macro.Replace(@"__POWER_CARD__", (power.CompendiumEntry != null) ? power.CompendiumEntry : string.Empty);
                    }

                    // encode all special characters as proper url supported characters
                    string encodedMacro = HttpUtility.UrlEncode(macro);

                    string macroCreation = MacroCreationTemplate;
                    macroCreation = macroCreation.Replace(@"__MACRO_NAME__", GetMacroName(power));
                    macroCreation = macroCreation.Replace(@"__MACRO_BACKGROUND_COLOR__", GetMacroBackgroundColor(power.Usage));
                    macroCreation = macroCreation.Replace(@"__MACRO_FOREGROUND_COLOR__", GetMacroForegroundColor(power.Usage));
                    macroCreation = macroCreation.Replace(@"__MACRO_GROUP__", GetMacroGroup(power.Usage));
                    macroCreation = macroCreation.Replace(@"__MACRO_CODE__", encodedMacro);

                    writer.WriteLine(macroCreation);

                    // separator for readability
                    writer.WriteLine(@"<!-- ======================================================================= -->");
                }
            }
        }
    }
}
