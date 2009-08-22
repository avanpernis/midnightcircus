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
        public static string HeaderTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.HeaderTemplate;
            }
        }

        public static string CheckTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.CheckTemplate;
            }
        }

        public static string SavingThrowTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.SavingThrowTemplate;
            }
        }

        public static string InitiativeTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.InitiativeTemplate;
            }
        }

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

        public static string LimitedUse
        {
            get
            {
                return global::TokenAssist.Properties.Resources.LimitedUseTemplate;
            }
        }

        public static string FeatTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.FeatTemplate;
            }
        }

        public static string MagicItemTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.MagicItemTemplate;
            }
        }

        private static string GetMacroName(Power power)
        {
            return string.Format(@"<b>{0}</b><br>{1} {2}", power.Name, power.Action.ToString(), power.AttackTypeAndRange);
        }

        private static string GetMacroName(Feat feat)
        {
            return string.Format(@"<b>{0}</b>", feat.Name);
        }

        private static string GetMacroName(MagicItem magicItem)
        {
            string macroName = string.Format(@"<b>{0}</b>", magicItem.Name);

            if (magicItem.HasPower)
            {
                macroName += string.Format("<br>{0}", magicItem.PowerAction.ToString());
            }

            return macroName;
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

        private static string GetMacroBackgroundColor(Feat feat)
        {
            return "blue";
        }

        private static string GetMacroBackgroundColor(MagicItem magicItem)
        {
            return "orange";
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

        private static string GetMacroForegroundColor(Feat feat)
        {
            return "white";
        }

        private static string GetMacroForegroundColor(MagicItem magicItem)
        {
            return "black";
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

        private static string GetMacroGroup(Feat feat)
        {
            return "Feat";
        }

        private static string GetMacroGroup(MagicItem magicItem)
        {
            return "Magic Item";
        }

        private static string GetMacroGroup(MagicItem.PowerUsageType usageType)
        {
            switch (usageType)
            {
                case MagicItem.PowerUsageType.AtWill:
                    return "At-Will";
                case MagicItem.PowerUsageType.Encounter:
                    return "Encounter";
                case MagicItem.PowerUsageType.Daily:
                    return "Daily";
                case MagicItem.PowerUsageType.HealingSurge:
                    return "Healing-Surge";
                case MagicItem.PowerUsageType.Consumable:
                    return "Consumable";
                default:
                    return null;
            }
        }

        private static string GetAbilityCheckNameList()
        {
            return "Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma";
        }

        private static string GetAbilityCheckBonusList(Character character)
        {
            StringBuilder builder = new StringBuilder();

            int levelBonus = character.Stats["half-level"].Value;

            builder.Append((character.Stats["strength modifier"].Value + levelBonus).ToString() + ", ");
            builder.Append((character.Stats["dexterity modifier"].Value + levelBonus).ToString() + ", ");
            builder.Append((character.Stats["constitution modifier"].Value + levelBonus).ToString() + ", ");
            builder.Append((character.Stats["intelligence modifier"].Value + levelBonus).ToString() + ", ");
            builder.Append((character.Stats["wisdom modifier"].Value + levelBonus).ToString() + ", ");
            builder.Append((character.Stats["charisma modifier"].Value + levelBonus).ToString());

            return builder.ToString();
        }

        private static string GetSkillCheckNameList()
        {
            return "Acrobatics, Arcana, Athletics, Bluff, Diplomacy, Dungeoneering, Endurance, Heal, History, Insight, Intimidate, Nature, Perception, Religion, Stealth, Streetwise, Thievery";
        }

        private static string GetSkillCheckBonusList(Character character)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(character.Stats["acrobatics"].Value.ToString() + ", ");
            builder.Append(character.Stats["arcana"].Value.ToString() + ", ");
            builder.Append(character.Stats["athletics"].Value.ToString() + ", ");
            builder.Append(character.Stats["acrobatics"].Value.ToString() + ", ");
            builder.Append(character.Stats["bluff"].Value.ToString() + ", ");
            builder.Append(character.Stats["diplomacy"].Value.ToString() + ", ");
            builder.Append(character.Stats["dungeoneering"].Value.ToString() + ", ");
            builder.Append(character.Stats["endurance"].Value.ToString() + ", ");
            builder.Append(character.Stats["heal"].Value.ToString() + ", ");
            builder.Append(character.Stats["history"].Value.ToString() + ", ");
            builder.Append(character.Stats["insight"].Value.ToString() + ", ");
            builder.Append(character.Stats["intimidate"].Value.ToString() + ", ");
            builder.Append(character.Stats["nature"].Value.ToString() + ", ");
            builder.Append(character.Stats["perception"].Value.ToString() + ", ");
            builder.Append(character.Stats["religion"].Value.ToString() + ", ");
            builder.Append(character.Stats["stealth"].Value.ToString() + ", ");
            builder.Append(character.Stats["streetwise"].Value.ToString() + ", ");
            builder.Append(character.Stats["thievery"].Value.ToString() + ", ");

            return builder.ToString();
        }

        public static void Dump(Character character, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                string header = HeaderTemplate;
                header = header.Replace(@"__LEVEL__", character.Stats["level"].Value.ToString());

                writer.WriteLine(header);

                // separator for readability
                writer.WriteLine(@"<!-- ======================================================================= -->");

                // ability checks
                string abilityChecks = CheckTemplate;
                abilityChecks = abilityChecks.Replace(@"__CHECK_NAME_LIST__", GetAbilityCheckNameList());
                abilityChecks = abilityChecks.Replace(@"__CHECK_BONUS_LIST__", GetAbilityCheckBonusList(character));

                abilityChecks = FinalizeMacro(abilityChecks, "<b>Ability</b>", "white", "black", "1:Checks");

                writer.WriteLine(abilityChecks);

                // separator for readability
                writer.WriteLine(@"<!-- ======================================================================= -->");

                // skill checks
                string skillChecks = CheckTemplate;
                skillChecks = skillChecks.Replace(@"__CHECK_NAME_LIST__", GetSkillCheckNameList());
                skillChecks = skillChecks.Replace(@"__CHECK_BONUS_LIST__", GetSkillCheckBonusList(character));

                skillChecks = FinalizeMacro(skillChecks, "<b>Skill</b>", "white", "black", "1:Checks");

                writer.WriteLine(skillChecks);

                // separator for readability
                writer.WriteLine(@"<!-- ======================================================================= -->");

                // saving throw
                string savingThrow = SavingThrowTemplate;
                savingThrow = savingThrow.Replace(@"__SAVE_BONUS__", character.Stats["saving throws"].Value.ToString());

                savingThrow = FinalizeMacro(savingThrow, "<b>Saving Throw</b>", "white", "black", "1:Checks");

                writer.WriteLine(savingThrow);

                // separator for readability
                writer.WriteLine(@"<!-- ======================================================================= -->");

                // initiative
                string initiative = InitiativeTemplate;
                initiative = initiative.Replace(@"__INIT_BONUS__", character.Stats["initiative"].Value.ToString());

                initiative = FinalizeMacro(initiative, "<b>Initiative</b>", "white", "black", "1:Checks");

                writer.WriteLine(initiative);

                // separator for readability
                writer.WriteLine(@"<!-- ======================================================================= -->");

                int EncounterPowerCount = 0; // Keep track of Encounter Power IDs
                int DailyPowerCount = 0; // Keep track of Daily Power IDs

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

                        macro = WeaponTemplate;
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

                    if (power.Usage == Power.UsageType.Encounter || power.Usage == Power.UsageType.Daily)
                    {
                        string tempMacro = LimitedUse;
                        macro = tempMacro.Replace(@"__MACRO_TEXT__",macro);
                        macro = macro.Replace(@"__MACRO_NAME__", GetMacroName(power));
                        switch (power.Usage)
                        {
                            case Power.UsageType.Encounter:
                                macro = macro.Replace(@"__POWER_ID__",string.Format("{0}",EncounterPowerCount++));
                                macro = macro.Replace(@"__USAGE_TYPE__","Encounter");
                                break;
                            case Power.UsageType.Daily:
                                macro = macro.Replace(@"__POWER_ID__",string.Format("{0}",DailyPowerCount++));
                                macro = macro.Replace(@"__USAGE_TYPE__","Daily");
                                break;
                        }
                    }

                    macro = FinalizeMacro(macro, GetMacroName(power), GetMacroBackgroundColor(power.Usage), GetMacroForegroundColor(power.Usage), GetMacroGroup(power.Usage));

                    writer.WriteLine(macro);

                    // separator for readability
                    writer.WriteLine(@"<!-- ======================================================================= -->");
                }

                foreach (Feat feat in character.Feats)
                {
                    string macro = FeatTemplate;
                    macro = macro.Replace(@"__FEAT_NAME__", feat.Name);
                    macro = macro.Replace(@"__FEAT_CARD__", (feat.CompendiumEntry != null) ? feat.CompendiumEntry : string.Empty);

                    macro = FinalizeMacro(macro, GetMacroName(feat), GetMacroBackgroundColor(feat), GetMacroForegroundColor(feat), GetMacroGroup(feat));

                    writer.WriteLine(macro);

                    // separator for readability
                    writer.WriteLine(@"<!-- ======================================================================= -->");
                }

                foreach (MagicItem magicItem in character.MagicItems)
                {
                    string macro = MagicItemTemplate;
                    macro = macro.Replace(@"__MAGIC_ITEM_NAME__", magicItem.Name);
                    macro = macro.Replace(@"__MAGIC_ITEM_CARD__", (magicItem.CompendiumEntry != null) ? magicItem.CompendiumEntry : string.Empty);

                    macro = FinalizeMacro(macro, GetMacroName(magicItem), GetMacroBackgroundColor(magicItem), GetMacroForegroundColor(magicItem), GetMacroGroup(magicItem));

                    writer.WriteLine(macro);

                    // separator for readability
                    writer.WriteLine(@"<!-- ======================================================================= -->");

                    if (magicItem.HasPower)
                    {
                        // create a separate macro for the power usage in the appropriate power macro group
                        macro = NoWeaponTemplate;

                        macro = macro.Replace(@"__POWER_NAME__", magicItem.Name);
                        macro = macro.Replace(@"__POWER_CARD__", (magicItem.CompendiumEntry != null) ? magicItem.CompendiumEntry : string.Empty);

                        if (magicItem.PowerUsage == MagicItem.PowerUsageType.Encounter || magicItem.PowerUsage == MagicItem.PowerUsageType.Daily)
                        {
                            string tempMacro = LimitedUse;
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
                                    break;
                            }
                        }

                        macro = FinalizeMacro(macro, GetMacroName(magicItem), GetMacroBackgroundColor(magicItem), GetMacroForegroundColor(magicItem), GetMacroGroup(magicItem.PowerUsage));

                        writer.WriteLine(macro);

                        // separator for readability
                        writer.WriteLine(@"<!-- ======================================================================= -->");
                    }
                }
            }
        }

        private static string FinalizeMacro(string macro, string name, string backgroundColor, string foregroundColor, string group)
        {
            string macroCreation = MacroCreationTemplate;
            macroCreation = macroCreation.Replace(@"__MACRO_NAME__", name);
            macroCreation = macroCreation.Replace(@"__MACRO_BACKGROUND_COLOR__", backgroundColor);
            macroCreation = macroCreation.Replace(@"__MACRO_FOREGROUND_COLOR__", foregroundColor);
            macroCreation = macroCreation.Replace(@"__MACRO_GROUP__", group);

            // encode all special characters as proper url supported characters
            macroCreation = macroCreation.Replace(@"__MACRO_CODE__", HttpUtility.UrlEncode(macro));

            return macroCreation;
        }
    }
}
