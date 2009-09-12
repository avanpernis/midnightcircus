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

        public static string HealingTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.HealingTemplate;
            }
        }

        public static string DamageTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.DamageTemplate;
            }
        }

        public static string TempHPTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.TempHPTemplate;
            }
        }

        public static string RestTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.RestTemplate;
            }
        }

        public static string ActionPointTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.ActionPointTemplate;
            }
        }

        public static string MilestoneTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.MilestoneTemplate;
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

        public static string DailyItemTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.DailyItemTemplate;
            }
        }

        public static string MagicItemHealingSurgeTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.MagicItemHealingSurgeTemplate;
            }
        }

        public static string ConsumableTemplate
        {
            get
            {
                return global::TokenAssist.Properties.Resources.ConsumableTemplate;
            }
        }

        private static string GetCheckMacroName(string name)
        {
            return string.Format(@"<b>{0}</b>", name);
        }

        const string CheckMacroBackgroundColor = "white";
        const string CheckMacroForegroundColor = "black";
        const string CheckMacroGroup = "1:Check";

        private static string GetMiscMacroName(string name)
        {
            return string.Format(@"<b>{0}</b>", name);
        }

        const string MiscMacroBackgroundColor = "white";
        const string MiscMacroForegroundColor = "black";
        const string MiscMacroGroup = "2:Misc";

        private static string GetMacroName(Power power)
        {
            string name = power.Name;

            if ((power.CompendiumEntry != null) && (power.CompendiumEntry.Contains("Channel Divinity")))
            {
                name = string.Format("Channel Divinity: {0}", name);
            }

            return string.Format(@"<b>{0}</b><br>{1} {2}", name, power.Action.ToString(), power.AttackTypeAndRange);
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
                header = header.Replace(@"__SPEED__", character.Stats["speed"].Value.ToString());

                header = header.Replace(@"__STRENGTH__", character.Stats["strength"].Value.ToString());
                header = header.Replace(@"__DEXTERITY__", character.Stats["dexterity"].Value.ToString());
                header = header.Replace(@"__CONSTITUTION__", character.Stats["constitution"].Value.ToString());
                header = header.Replace(@"__INTELLIGENCE__", character.Stats["intelligence"].Value.ToString());
                header = header.Replace(@"__WISDOM__", character.Stats["wisdom"].Value.ToString());
                header = header.Replace(@"__CHARISMA__", character.Stats["charisma"].Value.ToString());

                header = header.Replace(@"__STRENGTH_MODIFIER__", character.Stats["strength modifier"].Value.ToString());
                header = header.Replace(@"__DEXTERITY_MODIFIER__", character.Stats["dexterity modifier"].Value.ToString());
                header = header.Replace(@"__CONSTITUTION_MODIFIER__", character.Stats["constitution modifier"].Value.ToString());
                header = header.Replace(@"__INTELLIGENCE_MODIFIER__", character.Stats["intelligence modifier"].Value.ToString());
                header = header.Replace(@"__WISDOM_MODIFIER__", character.Stats["wisdom modifier"].Value.ToString());
                header = header.Replace(@"__CHARISMA_MODIFIER__", character.Stats["charisma modifier"].Value.ToString());

                header = header.Replace(@"__AC__", character.Stats["ac"].Value.ToString());
                header = header.Replace(@"__FORTITUDE__", character.Stats["fortitude defense"].Value.ToString());
                header = header.Replace(@"__REFLEX__", character.Stats["reflex defense"].Value.ToString());
                header = header.Replace(@"__WILL__", character.Stats["will defense"].Value.ToString());

                header = header.Replace(@"__MAX_HIT_POINTS__", character.Stats["hit points"].Value.ToString());
                header = header.Replace(@"__HEALING_SURGES_PER_DAY__", character.Stats["healing surges"].Value.ToString());
                header = header.Replace(@"__HEALING_SURGE_BONUS__", character.GetStatValue("healing surge value").ToString());

                header = header.Replace(@"__ACROBATICS__", character.Stats["acrobatics"].Value.ToString());
                header = header.Replace(@"__ARCANA__", character.Stats["arcana"].Value.ToString());
                header = header.Replace(@"__ATHLETICS__", character.Stats["athletics"].Value.ToString());
                header = header.Replace(@"__BLUFF__", character.Stats["bluff"].Value.ToString());
                header = header.Replace(@"__DIPLOMACY__", character.Stats["diplomacy"].Value.ToString());
                header = header.Replace(@"__DUNGEONEERING__", character.Stats["dungeoneering"].Value.ToString());
                header = header.Replace(@"__ENDURANCE__", character.Stats["endurance"].Value.ToString());
                header = header.Replace(@"__HEAL__", character.Stats["heal"].Value.ToString());
                header = header.Replace(@"__HISTORY__", character.Stats["history"].Value.ToString());
                header = header.Replace(@"__INSIGHT__", character.Stats["insight"].Value.ToString());
                header = header.Replace(@"__INTIMIDATE__", character.Stats["intimidate"].Value.ToString());
                header = header.Replace(@"__NATURE__", character.Stats["nature"].Value.ToString());
                header = header.Replace(@"__PERCEPTION__", character.Stats["perception"].Value.ToString());
                header = header.Replace(@"__RELIGION__", character.Stats["religion"].Value.ToString());
                header = header.Replace(@"__STEALTH__", character.Stats["stealth"].Value.ToString());
                header = header.Replace(@"__STREETWISE__", character.Stats["streetwise"].Value.ToString());
                header = header.Replace(@"__THIEVERY__", character.Stats["thievery"].Value.ToString());

                writer.WriteLine(header);

                // ability checks
                string abilityChecks = CheckTemplate;
                abilityChecks = abilityChecks.Replace(@"__CHECK_NAME_LIST__", GetAbilityCheckNameList());
                abilityChecks = abilityChecks.Replace(@"__CHECK_BONUS_LIST__", GetAbilityCheckBonusList(character));

                abilityChecks = FinalizeMacro(abilityChecks, GetCheckMacroName("Ability"), CheckMacroBackgroundColor, CheckMacroForegroundColor, CheckMacroGroup);

                writer.WriteLine(abilityChecks);

                // skill checks
                string skillChecks = CheckTemplate;
                skillChecks = skillChecks.Replace(@"__CHECK_NAME_LIST__", GetSkillCheckNameList());
                skillChecks = skillChecks.Replace(@"__CHECK_BONUS_LIST__", GetSkillCheckBonusList(character));

                skillChecks = FinalizeMacro(skillChecks, GetCheckMacroName("Skill"), CheckMacroBackgroundColor, CheckMacroForegroundColor, CheckMacroGroup);

                writer.WriteLine(skillChecks);

                // saving throw
                string savingThrow = SavingThrowTemplate;
                savingThrow = savingThrow.Replace(@"__SAVE_BONUS__", character.GetStatValue("saving throws").ToString());

                savingThrow = FinalizeMacro(savingThrow, GetCheckMacroName("Saving Throw"), CheckMacroBackgroundColor, CheckMacroForegroundColor, CheckMacroGroup);

                writer.WriteLine(savingThrow);

                // initiative
                string initiative = InitiativeTemplate;
                initiative = initiative.Replace(@"__INIT_BONUS__", character.Stats["initiative"].Value.ToString());

                initiative = FinalizeMacro(initiative, GetCheckMacroName("Initiative"), CheckMacroBackgroundColor, CheckMacroForegroundColor, CheckMacroGroup);

                writer.WriteLine(initiative);

                // healing
                string healing = FinalizeMacro(HealingTemplate, GetMiscMacroName("Healing"), MiscMacroBackgroundColor, MiscMacroForegroundColor, MiscMacroGroup);

                writer.WriteLine(healing);

                // damage
                string damage = FinalizeMacro(DamageTemplate, GetMiscMacroName("Damage"), MiscMacroBackgroundColor, MiscMacroForegroundColor, MiscMacroGroup);

                writer.WriteLine(damage);

                // temp hit points
                string temphp = FinalizeMacro(TempHPTemplate, GetMiscMacroName("Temp HP"), MiscMacroBackgroundColor, MiscMacroForegroundColor, MiscMacroGroup);

                writer.WriteLine(temphp);

                // rest
                string rest = FinalizeMacro(RestTemplate, GetMiscMacroName("Rest"), MiscMacroBackgroundColor, MiscMacroForegroundColor, MiscMacroGroup);

                writer.WriteLine(rest);

                // action point
                string actionPoint = FinalizeMacro(ActionPointTemplate, GetMiscMacroName("Action Point"), MiscMacroBackgroundColor, MiscMacroForegroundColor, MiscMacroGroup);

                writer.WriteLine(actionPoint);

                // milestone
                string milestone = FinalizeMacro(MilestoneTemplate, GetMiscMacroName("Milestone"), MiscMacroBackgroundColor, MiscMacroForegroundColor, MiscMacroGroup);

                writer.WriteLine(milestone);

                int ChannelDivinityPowerId = 0; // all channel divinity powers will use the same power id
                int EncounterPowerCount = 1; // all other encounter powers will start at id #1
                int DailyPowerCount = 0; // daily powers can start at id #0

                foreach (Power power in character.Powers)
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
                        macro = tempMacro.Replace(@"__MACRO_TEXT__", macro);
                        switch (power.Usage)
                        {
                            case Power.UsageType.Encounter:
                                {
                                    bool channelDivinity = (power.CompendiumEntry != null) && (power.CompendiumEntry.Contains("Channel Divinity"));
                                    macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", channelDivinity ? ChannelDivinityPowerId : EncounterPowerCount++));
                                    macro = macro.Replace(@"__USAGE_TYPE__", "Encounter");
                                    break;
                                }
                            case Power.UsageType.Daily:
                                macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", DailyPowerCount++));
                                macro = macro.Replace(@"__USAGE_TYPE__", "Daily");
                                break;
                        }
                    }

                    macro = FinalizeMacro(macro, GetMacroName(power), GetMacroBackgroundColor(power.Usage), GetMacroForegroundColor(power.Usage), GetMacroGroup(power.Usage));

                    writer.WriteLine(macro);
                }

                foreach (Feat feat in character.Feats)
                {
                    string macro = FeatTemplate;
                    macro = macro.Replace(@"__FEAT_NAME__", feat.Name);
                    macro = macro.Replace(@"__FEAT_CARD__", (feat.CompendiumEntry != null) ? feat.CompendiumEntry : string.Empty);

                    macro = FinalizeMacro(macro, GetMacroName(feat), GetMacroBackgroundColor(feat), GetMacroForegroundColor(feat), GetMacroGroup(feat));

                    writer.WriteLine(macro);
                }

                int HealingSurgeMagicItemCount = 0; // Keep track of Magic Item IDs for items that can be recharged with a healing surge

                foreach (MagicItem magicItem in character.MagicItems)
                {
                    string macro = MagicItemTemplate;
                    macro = macro.Replace(@"__MAGIC_ITEM_NAME__", magicItem.Name);
                    macro = macro.Replace(@"__MAGIC_ITEM_CARD__", (magicItem.CompendiumEntry != null) ? magicItem.CompendiumEntry : string.Empty);

                    macro = FinalizeMacro(macro, GetMacroName(magicItem), GetMacroBackgroundColor(magicItem), GetMacroForegroundColor(magicItem), GetMacroGroup(magicItem));

                    writer.WriteLine(macro);

                    if (magicItem.HasPower)
                    {
                        // create a separate macro for the power usage in the appropriate power macro group
                        if (magicItem.PowerUsage == MagicItem.PowerUsageType.HealingSurge)
                        {
                            // healing surges require a special sort of limited use macro
                            macro = MagicItemHealingSurgeTemplate;

                            macro = macro.Replace(@"__POWER_ID__", string.Format("{0}", HealingSurgeMagicItemCount++));
                            macro = macro.Replace(@"__MACRO_TEXT__", NoWeaponTemplate);
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

                                        // now wrap the macro one last time with the daily item template
                                        macro = DailyItemTemplate.Replace(@"__MACRO_TEXT__", macro);
                                        break;
                                }
                            }
                        }

                        macro = FinalizeMacro(macro, GetMacroName(magicItem), GetMacroBackgroundColor(magicItem), GetMacroForegroundColor(magicItem), GetMacroGroup(magicItem.PowerUsage));

                        writer.WriteLine(macro);
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
