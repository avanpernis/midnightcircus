using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public class CharacterTokenBuilder : Builder
    {
        public static string ClassFeatureGroup = "Class Feature";
        public static string FeatGroup = "Feat";
        public static string MagicItemGroup = "Magic Item";

        public static void WriteToken(Character character, string filename, string tokenImage, string tokenPortrait)
        {
            Token token = ActorTokenFactory.Create(character, "4ePlayer", tokenImage, tokenPortrait);

            // only characters use daily item powers
            token.AddProperty("DailyItemUses", character.DailyItemuses);

            // only characters take rests and finish milestones
            token.AddMacro(HtmlUtilities.Bold("Rest"), ActorTokenFactory.MiscGroup, Color.white, Color.black, Properties.Resources.RestTemplate);
            token.AddMacro(HtmlUtilities.Bold("Milestone"), ActorTokenFactory.MiscGroup, Color.white, Color.black, Properties.Resources.MilestoneTemplate);

            foreach (ClassFeature classFeature in character.ClassFeatures)
            {
                string macro = Properties.Resources.ClassFeatureTemplate;
                macro = macro.Replace(@"__CLASS_FEATURE_NAME__", classFeature.Name);
                macro = macro.Replace(@"__CLASS_FEATURE_DESCRIPTION__", classFeature.Description);

                token.AddMacro(HtmlUtilities.Bold(classFeature.Name), ClassFeatureGroup, Color.purple, Color.white, macro);
            }

            int ChannelDivinityPowerId = 0; // all channel divinity powers will use the same power id
            int EncounterPowerCount = 1; // all other encounter powers will start at id #1
            int DailyPowerCount = 0; // daily powers can start at id #0

            foreach (CharacterPower power in character.Powers)
            {
                string macro = null;

                bool noWeapons = true;

                // if all the weapons have unknown attack/defense, this is a power with no weapon component
                foreach (Weapon weapon in power.Weapons)
                {
                    if ((weapon.AttackStat != "Unknown") || (weapon.Defense != "Unknown"))
                    {
                        noWeapons = false;
                        break;
                    }
                }

                if (noWeapons)
                {
                    // this power does not involve weapons -- use the appropriate template

                    macro = Properties.Resources.NoWeaponTemplate;
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
                    string criticalDamageList = string.Empty;
                    string attackStatList = string.Empty;
                    string defenseList = string.Empty;

                    foreach (Weapon weapon in power.Weapons)
                    {
                        weaponList += (weaponList.Length == 0) ? weapon.Name : string.Format(", {0}", weapon.Name);
                        attackBonusList += (attackBonusList.Length == 0) ? weapon.AttackBonus.ToString() : string.Format(", {0}", weapon.AttackBonus);
                        damageList += (damageList.Length == 0) ? weapon.Damage : string.Format(", {0}", weapon.Damage);
                        maxDamageList += (maxDamageList.Length == 0) ? weapon.MaxDamage : string.Format(", {0}", weapon.MaxDamage);
                        criticalDamageList += (criticalDamageList.Length == 0) ? weapon.CriticalDamage : string.Format(", {0}", weapon.CriticalDamage);
                        attackStatList += (attackStatList.Length == 0) ? weapon.AttackStat.ToString() : string.Format(", {0}", weapon.AttackStat.ToString());
                        defenseList += (defenseList.Length == 0) ? weapon.Defense.ToString() : string.Format(", {0}", weapon.Defense.ToString());
                    }

                    macro = Properties.Resources.WeaponTemplate;
                    macro = macro.Replace(@"__POWER_NAME__", power.Name);
                    macro = macro.Replace(@"__WEAPON_LIST__", weaponList);
                    macro = macro.Replace(@"__ATTACK_BONUS_LIST__", attackBonusList);
                    macro = macro.Replace(@"__DAMAGE_LIST__", damageList);
                    macro = macro.Replace(@"__MAX_DAMAGE_LIST__", maxDamageList);
                    macro = macro.Replace(@"__CRITICAL_DAMAGE_LIST__", criticalDamageList);
                    macro = macro.Replace(@"__ATTACK_STAT_LIST__", attackStatList);
                    macro = macro.Replace(@"__DEFENSE_LIST__", defenseList);
                    macro = macro.Replace(@"__MULTIPLE_TARGETS__", power.AllowsForMultipleAttacks ? "1" : "0");
                    macro = macro.Replace(@"__POWER_CARD__", (power.CompendiumEntry != null) ? power.CompendiumEntry : string.Empty);
                }

                // insert macro into Encounter or Daily macro

                if (power.Usage == CharacterPower.UsageType.Encounter || power.Usage == CharacterPower.UsageType.Daily)
                {
                    string tempMacro = Properties.Resources.LimitedUseTemplate;
                    macro = tempMacro.Replace(@"__MACRO_TEXT__", macro);
                    switch (power.Usage)
                    {
                        case CharacterPower.UsageType.Encounter:
                            {
                                bool channelDivinity = (power.CompendiumEntry != null) && (power.CompendiumEntry.Contains("Channel Divinity"));
                                macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", channelDivinity ? ChannelDivinityPowerId : EncounterPowerCount++));
                                macro = macro.Replace(@"__USAGE_TYPE__", "Encounter");
                                break;
                            }
                        case CharacterPower.UsageType.Daily:
                            macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", DailyPowerCount++));
                            macro = macro.Replace(@"__USAGE_TYPE__", "Daily");
                            break;
                    }
                }

                token.AddMacro(GetMacroName(power), GetMacroGroup(power), GetMacroButtonColor(power), GetMacroFontColor(power), macro);
            }

            foreach (Feat feat in character.Feats)
            {
                string macro = Properties.Resources.FeatTemplate;
                macro = macro.Replace(@"__FEAT_NAME__", feat.Name);
                macro = macro.Replace(@"__FEAT_CARD__", (feat.CompendiumEntry != null) ? feat.CompendiumEntry : string.Empty);

                token.AddMacro(HtmlUtilities.Bold(feat.Name), FeatGroup, Color.blue, Color.white, macro);
            }

            int HealingSurgeMagicItemCount = 0; // Keep track of Magic Item IDs for items that can be recharged with a healing surge

            foreach (MagicItem magicItem in character.MagicItems)
            {
                string macro = Properties.Resources.MagicItemTemplate;
                macro = macro.Replace(@"__MAGIC_ITEM_NAME__", magicItem.Name);
                macro = macro.Replace(@"__MAGIC_ITEM_CARD__", (magicItem.CompendiumEntry != null) ? magicItem.CompendiumEntry : string.Empty);

                token.AddMacro(GetMacroName(magicItem), MagicItemGroup, Color.orange, Color.black, macro);

                if (magicItem.HasPower)
                {
                    // create a separate macro for the power usage in the appropriate power macro group
                    if (magicItem.PowerUsage == MagicItem.PowerUsageType.HealingSurge)
                    {
                        // healing surges require a special sort of limited use macro
                        macro = Properties.Resources.MagicItemHealingSurgeTemplate;

                        macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", HealingSurgeMagicItemCount++));
                        macro = macro.Replace(@"__MACRO_TEXT__", Properties.Resources.NoWeaponTemplate);
                        macro = macro.Replace(@"__POWER_NAME__", magicItem.Name);
                        macro = macro.Replace(@"__POWER_CARD__", (magicItem.CompendiumEntry != null) ? magicItem.CompendiumEntry : string.Empty);
                    }
                    // TODO: we need to handle consumables differently
                    //else if (magicItem.PowerUsage == MagicItem.PowerUsageType.Consumable)
                    //{
                    //}
                    else
                    {
                        // at-will, encounter, and daily magic item uses can be handled similarly to normal powers
                        macro = Properties.Resources.NoWeaponTemplate;

                        macro = macro.Replace(@"__POWER_NAME__", magicItem.Name);
                        macro = macro.Replace(@"__POWER_CARD__", (magicItem.CompendiumEntry != null) ? magicItem.CompendiumEntry : string.Empty);

                        if (magicItem.PowerUsage == MagicItem.PowerUsageType.Encounter || magicItem.PowerUsage == MagicItem.PowerUsageType.Daily)
                        {
                            string tempMacro = Properties.Resources.LimitedUseTemplate;
                            macro = tempMacro.Replace(@"__MACRO_TEXT__", macro);
                            macro = macro.Replace(@"__MACRO_NAME__", GetMacroName(magicItem));
                            switch (magicItem.PowerUsage)
                            {
                                case MagicItem.PowerUsageType.Encounter:
                                    macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", EncounterPowerCount++));
                                    macro = macro.Replace(@"__USAGE_TYPE__", "Encounter");
                                    break;
                                case MagicItem.PowerUsageType.Daily:
                                    macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", DailyPowerCount++));
                                    macro = macro.Replace(@"__USAGE_TYPE__", "Daily");

                                    // now wrap the macro one last time with the daily item template
                                    macro = Properties.Resources.DailyItemTemplate.Replace(@"__MACRO_TEXT__", macro);
                                    break;
                            }
                        }
                    }

                    token.AddMacro(GetMacroName(magicItem), GetMacroGroup(magicItem), Color.orange, Color.black, macro);
                }
            }

            token.Write(filename);
        }

        private static string GetMacroName(CharacterPower power)
        {
            string name = power.Name;

            if ((power.CompendiumEntry != null) && (power.CompendiumEntry.Contains("Channel Divinity")))
            {
                name = string.Format("Channel Divinity: {0}", name);
            }

            return string.Format(@"{0}<br>{1} {2}", HtmlUtilities.Bold(name), power.Action.ToString(), power.AttackTypeAndRange);
        }

	    private static string GetMacroGroup(CharacterPower power)
        {
            switch (power.Usage)
            {
                case CharacterPower.UsageType.AtWill:
                    return "3:At-Will";
                case CharacterPower.UsageType.Encounter:
                    return "4:Encounter";
                case CharacterPower.UsageType.Daily:
                    return "5:Daily";
                default:
                    return null;
            }
        }    
        
        private static string GetMacroName(MagicItem magicItem)
        {
            string macroName = HtmlUtilities.Bold(magicItem.Name);

            if (magicItem.HasPower)
            {
                macroName += string.Format("<br>{0}", magicItem.PowerAction.ToString());
            }

            return macroName;
        }

        private static string GetMacroGroup(MagicItem magicItem)
        {
            switch (magicItem.PowerUsage)
            {
                case MagicItem.PowerUsageType.AtWill:
                    return "3:At-Will";
                case MagicItem.PowerUsageType.Encounter:
                    return "4:Encounter";
                case MagicItem.PowerUsageType.Daily:
                    return "5:Daily";
                case MagicItem.PowerUsageType.HealingSurge:
                    return "6:Healing-Surge";
                case MagicItem.PowerUsageType.Consumable:
                    return "6:Consumable";
                default:
                    return null;
            }
        }
    }
}
