using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace TokenAssist
{
    public static class CharacterLoader
    {
        public static Character Load(string filename)
        {
            Character character = new Character();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);

            XmlNode xmlNodeDetails = xmlDocument.SelectSingleNode("//Details");
            character.Name = GetDescendantNodeText(xmlNodeDetails, "name");

            // we use this to get the url information for powers and feats
            XmlNode xmlNodeUrlsRoot = xmlDocument.SelectSingleNode("//RulesElementTally");

            // look for all powers
            XmlNode xmlNodePowersRoot = xmlDocument.SelectSingleNode("//PowerStats");

            foreach (XmlNode xmlNodePower in xmlNodePowersRoot.ChildNodes)
            {
                Power power = LoadPower(xmlNodePower);
                power.Url = GetPowerUrl(xmlNodeUrlsRoot, power.Name);

                if (power.Url != null)
                {
                    power.CompendiumEntry = CompendiumUtilities.GetEntry(power.Url);

                    if (power.CompendiumEntry != null)
                    {
                        // we need to do these AFTER getting the compendium entry because it is the only place
                        // that contains this information.
                        power.AttackTypeAndRange = GetPowerAttackTypeAndRange(power.CompendiumEntry);
                        power.AllowsForMultipleAttacks = GetPowerAllowsForMultipleAttacks(power.CompendiumEntry);
                    }
                }

                character.Powers.Add(power);
            }

            // look for all feats
            XmlNodeList xmlNodeListFeats = xmlNodeUrlsRoot.SelectNodes("RulesElement[@type='Feat']");

            foreach (XmlNode xmlnodeFeat in xmlNodeListFeats)
            {
                Feat feat = LoadFeat(xmlnodeFeat);

                character.Feats.Add(feat);
            }

            // look for all magic items
            XmlNodeList xmlNodeListMagicItems = xmlDocument.SelectNodes("//LootTally/loot[@count>0]/RulesElement[@type='Magic Item']");

            foreach (XmlNode xmlNodeMagicItem in xmlNodeListMagicItems)
            {
                MagicItem magicItem = LoadMagicItem(xmlNodeMagicItem);

                character.MagicItems.Add(magicItem);
            }

            return character;
        }

        private static Power LoadPower(XmlNode xmlNodePower)
        {
            Power power = new Power();

            power.Name = GetAttributeText(xmlNodePower, "name");
            power.Usage = GetPowerUsage(xmlNodePower);
            power.Action = GetPowerAction(xmlNodePower);

            XmlNodeList xmlNodeWeapons = xmlNodePower.SelectNodes("Weapon");

            foreach (XmlNode xmlNodeWeapon in xmlNodeWeapons)
            {
                Weapon weapon = LoadWeapon(xmlNodeWeapon);

                power.Weapons.Add(weapon);
            }

            return power;
        }

        private static Feat LoadFeat(XmlNode xmlNodeFeat)
        {
            Feat feat = new Feat();

            feat.Name = GetAttributeText(xmlNodeFeat, "name");
            feat.Url = GetAttributeText(xmlNodeFeat, "url");

            if (feat.Url != null)
            {
                feat.CompendiumEntry = CompendiumUtilities.GetEntry(feat.Url);
            }

            feat.ShortDescription = GetDescendantNodeText(xmlNodeFeat, "specific[@name='Short Description']");

            return feat;
        }

        private static MagicItem LoadMagicItem(XmlNode xmlNodeMagicItem)
        {
            MagicItem magicItem = new MagicItem();

            magicItem.Name = GetAttributeText(xmlNodeMagicItem, "name");
            magicItem.Url = GetAttributeText(xmlNodeMagicItem, "url");

            if (magicItem.Url != null)
            {
                magicItem.CompendiumEntry = CompendiumUtilities.GetEntry(magicItem.Url);
            }

            return magicItem;
        }

        private static Weapon LoadWeapon(XmlNode xmlNodeWeapon)
        {
            Weapon weapon = new Weapon();

            weapon.Name = GetAttributeText(xmlNodeWeapon, "name");
            weapon.AttackBonus = GetWeaponAttackBonus(xmlNodeWeapon);
            weapon.Damage = GetWeaponDamage(xmlNodeWeapon);
            weapon.CriticalDamage = GetWeaponCriticalDamage(xmlNodeWeapon);
            weapon.AttackStat = GetWeaponAttackStat(xmlNodeWeapon);
            weapon.Defense = GetWeaponDefense(xmlNodeWeapon);

            return weapon;
        }

        private static Power.UsageType GetPowerUsage(XmlNode xmlNodePower)
        {
            string usage = GetDescendantNodeText(xmlNodePower, "specific[@name='Power Usage']");
            usage = usage.Replace("-", ""); // remove the hyphen so that we can enum parse for 'at-will'

            return (usage != null) ? (Power.UsageType)Enum.Parse(typeof(Power.UsageType), usage) : Power.UsageType.Undefined;
        }

        private static Power.ActionType GetPowerAction(XmlNode xmlNodePower)
        {
            string action = GetDescendantNodeText(xmlNodePower, "specific[@name='Action Type']");
            action = action.Replace("Action", ""); // remove "Action" suffix so that we can enum parse for 'free', 'minor', 'move', and 'standard
            action = action.Replace(" ", ""); // remove spaces so that we can enum parse for 'immediate interrupt' and 'immediate reaction'

            return (action != null) ? (Power.ActionType)Enum.Parse(typeof(Power.ActionType), action) : Power.ActionType.Undefined;
        }

        private static string GetPowerUrl(XmlNode xmlNodeUrls, string name)
        {
            // using \" here because some names have that darned apostrophe
            string url = GetDescendantAttributeText(xmlNodeUrls, string.Format("RulesElement[@name=\"{0}\" and @type=\"Power\"]", name), "url");

            return (url != null) ? url : null;
        }

        private static string GetPowerAttackTypeAndRange(string entry)
        {
            // find all paragraph blocks
            Regex pPattern = new Regex(@"<p[^>]*>(.*?)</p>");
            MatchCollection pMatches = pPattern.Matches(entry);

            // using the second paragraph block, search for all bold blocks (and trailing cruft)
            Regex attackPattern = new Regex(@"<b>.*?</b>[^<]*");
            MatchCollection matches = attackPattern.Matches(pMatches[1].Groups[1].Value);
            string result = matches[matches.Count - 1].ToString();

            // replace all tags with empty string
            Regex tagsPattern = new Regex("<[^>]*>");
            return tagsPattern.Replace(result, "").Trim();
        }

        private static bool GetPowerAllowsForMultipleAttacks(string entry)
        {
            Regex targetPattern = new Regex(@"<p[^>]*><b>\s*Target\s*</b>\s*:?\s*(.*?)</p>");
            Match match = targetPattern.Match(entry);

            bool allowsForMultipleAttacks = false;

            if (match.Success)
            {
                string targetInfo = match.Groups[1].Value;

                allowsForMultipleAttacks |= targetInfo.Contains("Each enemy");
                allowsForMultipleAttacks |= targetInfo.Contains("Each creature");
            }

            return allowsForMultipleAttacks;
        }

        private static int GetWeaponAttackBonus(XmlNode xmlNodeWeapon)
        {
            string attackBonus = GetDescendantNodeText(xmlNodeWeapon, "AttackBonus");

            return (attackBonus != null) ? int.Parse(attackBonus) : int.MinValue;
        }

        private static string GetWeaponDamage(XmlNode xmlNodeWeapon)
        {
            string damage = GetDescendantNodeText(xmlNodeWeapon, "Damage");

            return (damage != null) ? damage : null;
        }

        private static string GetWeaponCriticalDamage(XmlNode xmlNodeWeapon)
        {
            // get the url for the magic weapon
            string url = GetDescendantAttributeText(xmlNodeWeapon, "RulesElement[@type='Magic Item']", "url");

            if (url == null)
            {
                // not a magic weapon, so no critical damage
                return "0";
            }

            string compendiumEntry = CompendiumUtilities.GetEntry(url);

            if (compendiumEntry == null)
            {
                return "0";
            }

            Regex criticalPattern = new Regex(@"<b>Critical</b>:\s*([^\s]*)");
            Match match = criticalPattern.Match(compendiumEntry);

            return match.Success ? match.Groups[1].Value : "0";
        }

        private static Weapon.AttackStatType GetWeaponAttackStat(XmlNode xmlNodeWeapon)
        {
            string attackStat = GetDescendantNodeText(xmlNodeWeapon, "AttackStat");

            return (attackStat != null) ? (Weapon.AttackStatType)Enum.Parse(typeof(Weapon.AttackStatType), attackStat) : Weapon.AttackStatType.Undefined;
        }

        private static Weapon.DefenseType GetWeaponDefense(XmlNode xmlNodeWeapon)
        {
            string defense = GetDescendantNodeText(xmlNodeWeapon, "Defense");

            return (defense != null) ? (Weapon.DefenseType)Enum.Parse(typeof(Weapon.DefenseType), defense) : Weapon.DefenseType.Undefined;
        }

        private static string GetDescendantAttributeText(XmlNode xmlNodeParent, string xPath, string attributeName)
        {
            XmlNode xmlNodeDescendant = xmlNodeParent.SelectSingleNode(xPath);

            return (xmlNodeDescendant != null) ? GetAttributeText(xmlNodeDescendant, attributeName) : null;
        }

        private static string GetAttributeText(XmlNode xmlNode, string attributeName)
        {
            XmlAttribute xmlAttribute = xmlNode.Attributes[attributeName];

            return (xmlAttribute != null) ? xmlAttribute.InnerText.Trim() : null;
        }

        private static string GetDescendantNodeText(XmlNode xmlNodeParent, string xPath)
        {
            XmlNode xmlNodeDescendant = xmlNodeParent.SelectSingleNode(xPath);

            return (xmlNodeDescendant != null) ? xmlNodeDescendant.InnerText.Trim() : null;
        }
    }
}
