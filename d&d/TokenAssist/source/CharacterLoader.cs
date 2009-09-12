using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace TokenAssist
{
    public static class CharacterLoader
    {
        public static string MeleeBasicAttack
        {
            get
            {
                return global::TokenAssist.Properties.Resources.MeleeBasicAttack;
            }
        }

        public static string RangedBasicAttack
        {
            get
            {
                return global::TokenAssist.Properties.Resources.RangedBasicAttack;
            }
        }

        public static string SecondWind
        {
            get
            {
                return global::TokenAssist.Properties.Resources.SecondWind;
            }
        }

        public static Character Load(string filename)
        {
            // Make a temporary copy of the character file to work on
            filename = MakeTempCopy(filename);

            Character character = new Character();

            CompendiumUtilities.ActiveCharacter = character;

            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(filename);
            }
            catch (XmlException e)
            {
                MessageBox.Show(e.Message, "Error Reading Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return character;
            }

            XmlNode xmlNodeDetails = xmlDocument.SelectSingleNode("//Details");
            character.Name = GetDescendantNodeText(xmlNodeDetails, "name");

            // load in the character's Stat Block
            XmlNode xmlNodeStatBlock = xmlDocument.SelectSingleNode("//StatBlock");

            foreach (XmlNode xmlNodeStat in xmlNodeStatBlock.ChildNodes)
            {
                Stat stat = LoadStat(xmlNodeStat);

                character.Stats.Add(stat.Name, stat);
            }

            // we use this to get the url information for many things
            XmlNode xmlNodeRules = xmlDocument.SelectSingleNode("//RulesElementTally");

            character.Race = GetRace(xmlNodeRules);
            character.Class = GetClass(xmlNodeRules);

            // look for all powers
            XmlNode xmlNodePowersRoot = xmlDocument.SelectSingleNode("//PowerStats");

            foreach (XmlNode xmlNodePower in xmlNodePowersRoot.ChildNodes)
            {
                Power power = LoadPower(xmlNodePower);
                power.Url = GetPowerUrl(xmlNodeRules, power.Name);

                if (power.Url != null)
                {
                    power.CompendiumEntry = CompendiumUtilities.GetPower(power.Url);

                    if (power.CompendiumEntry != null)
                    {
                        // we need to do these AFTER getting the compendium entry because it is the only place
                        // that contains this information.
                        power.AttackTypeAndRange = GetPowerAttackTypeAndRange(power.CompendiumEntry);
                        power.AllowsForMultipleAttacks = GetPowerAllowsForMultipleAttacks(power.CompendiumEntry);
                    }
                }

                if (power.Name == "Melee Basic Attack")
                {
                    // special case: add a power card manually
                    power.CompendiumEntry = CompendiumUtilities.ApplyFormatting(MeleeBasicAttack);
                    power.AttackTypeAndRange = "Melee weapon";
                }
                else if (power.Name == "Ranged Basic Attack")
                {
                    // special case: add a power card manually
                    power.CompendiumEntry = CompendiumUtilities.ApplyFormatting(RangedBasicAttack);
                    power.AttackTypeAndRange = "Ranged weapon";
                }

                character.Powers.Add(power);
            }

            // every character gets a second wind power -- set it up with the standard properties (can be overridden per character if needed)
            Power secondWind = new Power();
            secondWind.Name = "Second Wind";
            secondWind.Usage = Power.UsageType.Encounter;
            secondWind.Action = Power.ActionType.Standard;
            secondWind.CompendiumEntry = CompendiumUtilities.ApplyFormatting(SecondWind);
            secondWind.AttackTypeAndRange = GetPowerAttackTypeAndRange(secondWind.CompendiumEntry);
            secondWind.AllowsForMultipleAttacks = GetPowerAllowsForMultipleAttacks(secondWind.CompendiumEntry);
            character.Powers.Add(secondWind);

            // look for all feats
            XmlNodeList xmlNodeListFeats = xmlNodeRules.SelectNodes("RulesElement[@type='Feat']");

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


            // Remove the temporary copy of the file
            File.Delete(filename);

            return character;
        }

        private static Race GetRace(XmlNode xmlNodeRules)
        {
            XmlNode xmlNodeRace = xmlNodeRules.SelectSingleNode("RulesElement[@type='Race']");

            Race race = new Race();
            race.Name = GetAttributeText(xmlNodeRace, "name");
            race.Url = GetAttributeText(xmlNodeRace, "url");

            return race;
        }

        private static Class GetClass(XmlNode xmlNodeRules)
        {
            XmlNode xmlNodeRace = xmlNodeRules.SelectSingleNode("RulesElement[@type='Class']");

            Class cls = new Class();
            cls.Name = GetAttributeText(xmlNodeRace, "name");
            cls.Url = GetAttributeText(xmlNodeRace, "url");

            return cls;
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

        private static Stat LoadStat(XmlNode xmlNodeStat)
        {
            Stat stat = new Stat();
            stat.Name = GetAttributeText(xmlNodeStat, "name").ToLower(); // seems to be some case issues between different files
            stat.Value = int.Parse(GetAttributeText(xmlNodeStat, "value"));

            return stat;
        }

        private static Feat LoadFeat(XmlNode xmlNodeFeat)
        {
            Feat feat = new Feat();

            feat.Name = GetAttributeText(xmlNodeFeat, "name");
            feat.Url = GetAttributeText(xmlNodeFeat, "url");

            if (feat.Url != null)
            {
                feat.CompendiumEntry = CompendiumUtilities.GetFeat(feat.Url);
            }

            feat.ShortDescription = GetDescendantNodeText(xmlNodeFeat, "specific[@name='Short Description']");

            return feat;
        }

        private static MagicItem LoadMagicItem(XmlNode xmlNodeMagicItem)
        {
            MagicItem magicItem = new MagicItem();

            magicItem.Name = GetAttributeText(xmlNodeMagicItem, "name");
            magicItem.LootCount = int.Parse(GetAttributeText(xmlNodeMagicItem.ParentNode, "count"));
            magicItem.Url = GetAttributeText(xmlNodeMagicItem, "url");

            if (magicItem.Url != null)
            {
                magicItem.CompendiumEntry = CompendiumUtilities.GetItem(magicItem.Url);

                if (magicItem.CompendiumEntry != null)
                {
                    // see if this magic item has a power
                    GetMagicItemPower(magicItem);
                }
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
            weapon.AttackStat = GetDescendantNodeText(xmlNodeWeapon, "AttackStat");
            weapon.Defense = GetDescendantNodeText(xmlNodeWeapon, "Defense");

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

            return GetAction(action);
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

            return string.IsNullOrEmpty(damage) ? "0" : damage;
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

            string compendiumEntry = CompendiumUtilities.GetItem(url);

            if (compendiumEntry == null)
            {
                return "0";
            }

            Regex criticalPattern = new Regex(@"<b>Critical</b>:\s*([^\s]*)");
            Match match = criticalPattern.Match(compendiumEntry);

            return match.Success ? match.Groups[1].Value : "0";
        }

        private static void GetMagicItemPower(MagicItem magicItem)
        {
            Regex usagePattern = new Regex(@"Power\s*\(([^)&]*)");
            Match usageMatch = usagePattern.Match(magicItem.CompendiumEntry);

            // this magic item may have a power -- if so, figure out its usage and action types
            if (usageMatch.Success)
            {
                string usage = usageMatch.Groups[1].Value.Trim();
                usage = usage.Replace("-", ""); // remove the hyphen so that we can enum parse for 'at-will'
                usage = usage.Replace(" ", ""); // remove spaces so that we can enum parse for 'healing surge'
                magicItem.PowerUsage = (MagicItem.PowerUsageType)Enum.Parse(typeof(MagicItem.PowerUsageType), usage);
            }

            Regex actionPattern = new Regex(@"Power\s*\([^:]*:([^.]*)");
            Match actionMatch = actionPattern.Match(magicItem.CompendiumEntry);

            if (actionMatch.Success)
            {
                magicItem.PowerAction = GetAction(actionMatch.Groups[1].Value.Trim());               
            }
        }

        private static Power.ActionType GetAction(string action)
        {
            action = action.Replace("Action", ""); // remove "Action" suffix so that we can enum parse for 'free', 'minor', 'move', and 'standard
            action = action.Replace(" ", ""); // remove spaces so that we can enum parse for 'immediate interrupt' and 'immediate reaction'

            return (Power.ActionType)Enum.Parse(typeof(Power.ActionType), action);
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

        /// <summary>
        /// The .dnd4e file may not be directly readable by the XmlDocument class. 
        /// Therefore we make a temporary copy and check for known incompatibilities,
        /// repairing those incompatibilities if found.
        /// </summary>
        /// <param name="filename">The filename of the .dnd4e file to make a copy of.</param>
        /// <returns>The full path of the temporary copy.</returns>
        private static string MakeTempCopy(string filename)
        {
            // Create a temporary file
            string tempFilename = Path.GetTempFileName();

            // Open the .dnd4e file for reading
            StreamReader input = new StreamReader(filename);

            // Open the temp file for writing
            StreamWriter output = new StreamWriter(tempFilename);

            // Create a regular expression to match names that have parentheses in them.
            // For example:
            //     <ID_INTERNAL_CLASS_FEATURE_VESTIGE_PACT_(HYBRID) />
            //     <ID_INTERNAL_FEAT_FOCUSED_EXPERTISE_(TRIPLE-HEADED_FLAIL) />
            //     <ID_INTERNAL_FEAT_FOCUSED_EXPERTISE_(XEN'DRIK_BOOMERANG) />
            Regex nameHasParens = new Regex(@"\x3C\w+\x28[\w'-]+\x29");

            // Copy the file
            string text;
            do
            {
                text = input.ReadLine();

                // Find names that have parentheses in them and remove the parentheses.
                if (text != null && nameHasParens.IsMatch(text))
                {
                    text = text.Replace("(", "");
                    text = text.Replace(")", "");
                    text = text.Replace("'", "-");
                }

                output.WriteLine(text);
            } while (text != null);
            input.Close();
            output.Close();
   
            return tempFilename;
        }
    }
}
