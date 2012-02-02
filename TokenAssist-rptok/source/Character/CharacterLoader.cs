using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace TokenAssist
{
    public class CharacterLoader : Loader
    {
        static public Character Load(string filename)
        {
            // Make a temporary copy of the character file to work on
            filename = MakeTempCopy(filename);

            CharacterLoader loader = new CharacterLoader(filename);
            return loader.Character;
        }

        public CharacterLoader(string filename)
        {
            CompendiumUtilities.ActiveCharacter = mCharacter;

            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(filename);
            }
            catch (XmlException e)
            {
                MessageBox.Show(e.Message, "Error Reading Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            XmlNode xmlNodeDetails = xmlDocument.SelectSingleNode("//Details");
            mCharacter.Name = GetDescendantNodeText(xmlNodeDetails, "name");
            mCharacter.Portrait = GetDescendantNodeText(xmlNodeDetails, "Portrait");

            // load in the character's Stat Block
            XmlNode xmlNodeStatBlock = xmlDocument.SelectSingleNode("//StatBlock");

            foreach (XmlNode xmlNodeStat in xmlNodeStatBlock.ChildNodes)
            {
                Stat stat = LoadStat(xmlNodeStat);

                switch (stat.Name)
                {
                    // Ability Scores
                    case "strength":
                        mCharacter.Abilities["Strength"].Value = stat.Value;
                        break;
                    case "constitution":
                        mCharacter.Abilities["Constitution"].Value = stat.Value;
                        break;
                    case "dexterity":
                        mCharacter.Abilities["Dexterity"].Value = stat.Value;
                        break;
                    case "intelligence":
                        mCharacter.Abilities["Intelligence"].Value = stat.Value;
                        break;
                    case "wisdom":
                        mCharacter.Abilities["Wisdom"].Value = stat.Value;
                        break;
                    case "charisma":
                        mCharacter.Abilities["Charisma"].Value = stat.Value;
                        break;

                    // Defenses
                    case "ac":
                        mCharacter.Defenses["AC"] = stat.Value;
                        break;
                    case "fortitude defense":
                        mCharacter.Defenses["Fortitude"] = stat.Value;
                        break;
                    case "reflex defense":
                        mCharacter.Defenses["Reflex"] = stat.Value;
                        break;
                    case "will defense":
                        mCharacter.Defenses["Will"] = stat.Value;
                        break;

                    // Skills
                    case "acrobatics":
                        mCharacter.Skills["Acrobatics"] = stat.Value;
                        break;
                    case "arcana":
                        mCharacter.Skills["Arcana"] = stat.Value;
                        break;
                    case "athletics":
                        mCharacter.Skills["Athletics"] = stat.Value;
                        break;
                    case "bluff":
                        mCharacter.Skills["Bluff"] = stat.Value;
                        break;
                    case "diplomacy":
                        mCharacter.Skills["Diplomacy"] = stat.Value;
                        break;
                    case "dungeoneering":
                        mCharacter.Skills["Dungeoneering"] = stat.Value;
                        break;
                    case "endurance":
                        mCharacter.Skills["Endurance"] = stat.Value;
                        break;
                    case "heal":
                        mCharacter.Skills["Heal"] = stat.Value;
                        break;
                    case "history":
                        mCharacter.Skills["History"] = stat.Value;
                        break;
                    case "insight":
                        mCharacter.Skills["Insight"] = stat.Value;
                        break;
                    case "intimidate":
                        mCharacter.Skills["Intimidate"] = stat.Value;
                        break;
                    case "nature":
                        mCharacter.Skills["Nature"] = stat.Value;
                        break;
                    case "perception":
                        mCharacter.Skills["Perception"] = stat.Value;
                        break;
                    case "religion":
                        mCharacter.Skills["Religion"] = stat.Value;
                        break;
                    case "stealth":
                        mCharacter.Skills["Stealth"] = stat.Value;
                        break;
                    case "streetwise":
                        mCharacter.Skills["Streetwise"] = stat.Value;
                        break;
                    case "thievery":
                        mCharacter.Skills["Thievery"] = stat.Value;
                        break;

                    // Misc. Stats
                    case "level":
                        mCharacter.Level = stat.Value;
                        break;
                    case "hit points":
                        mCharacter.HP = stat.Value;
                        break;
                    case "healing surges":
                        mCharacter.HealingSurges = stat.Value;
                        break;
                    case "initiative":
                        mCharacter.Initiative = stat.Value;
                        break;
                    case "_baseactionpoints":
                        mCharacter.ActionPoints = stat.Value;
                        break;
                    case "speed":
                        mCharacter.Speed = stat.Value;
                        break;
                    case "saving throws":
                        mCharacter.SavingThrow = stat.Value;
                        break;
                    default:
                        break;
                }
            }

            // we use this to get the url information for many things
            XmlNode xmlNodeRules = xmlDocument.SelectSingleNode("//RulesElementTally");

            mCharacter.Race = GetRace(xmlNodeRules);
            mCharacter.Class = GetClass(xmlNodeRules);

            // look for all class features
            XmlNodeList xmlNodeListClassFeatures = xmlNodeRules.SelectNodes("RulesElement[@type='Class Feature']");

            foreach (XmlNode xmlNodeClassFeature in xmlNodeListClassFeatures)
            {
                ClassFeature classFeature = LoadClassFeature(xmlNodeClassFeature);

                mCharacter.ClassFeatures.Add(classFeature);
            }

            // look for all powers
            XmlNode xmlNodePowersRoot = xmlDocument.SelectSingleNode("//PowerStats");

            foreach (XmlNode xmlNodePower in xmlNodePowersRoot.ChildNodes)
            {
                CharacterPower power = LoadPower(xmlNodePower);
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

                mCharacter.Powers.Add(power);
            }

            // every character gets a second wind power -- set it up with the standard properties (can be overridden per character if needed)
            CharacterPower secondWind = new CharacterPower();
            secondWind.Name = "Second Wind";
            secondWind.Usage = CharacterPower.UsageType.Encounter;
            secondWind.Action = CharacterPower.ActionType.Standard;
            secondWind.CompendiumEntry = CompendiumUtilities.ApplyFormatting(SecondWind);
            secondWind.AttackTypeAndRange = GetPowerAttackTypeAndRange(secondWind.CompendiumEntry);
            secondWind.AllowsForMultipleAttacks = GetPowerAllowsForMultipleAttacks(secondWind.CompendiumEntry);
            mCharacter.Powers.Add(secondWind);

            // look for all feats
            XmlNodeList xmlNodeListFeats = xmlNodeRules.SelectNodes("RulesElement[@type='Feat']");

            foreach (XmlNode xmlnodeFeat in xmlNodeListFeats)
            {
                Feat feat = LoadFeat(xmlnodeFeat);

                mCharacter.Feats.Add(feat);
            }

            // look for all magic items
            XmlNodeList xmlNodeListMagicItems = xmlDocument.SelectNodes("//LootTally/loot[@count>0]/RulesElement[@type='Magic Item']");

            foreach (XmlNode xmlNodeMagicItem in xmlNodeListMagicItems)
            {
                MagicItem magicItem = LoadMagicItem(xmlNodeMagicItem);

                mCharacter.MagicItems.Add(magicItem);
            }

            // Remove the temporary copy of the file
            File.Delete(filename);
        }

        private Race GetRace(XmlNode xmlNodeRules)
        {
            XmlNode xmlNodeRace = xmlNodeRules.SelectSingleNode("RulesElement[@type='Race']");

            Race race = new Race();
            race.Name = GetAttributeText(xmlNodeRace, "name");
            race.Url = GetAttributeText(xmlNodeRace, "url");

            return race;
        }

        private Class GetClass(XmlNode xmlNodeRules)
        {
            XmlNode xmlNodeRace = xmlNodeRules.SelectSingleNode("RulesElement[@type='Class']");

            Class cls = new Class();
            cls.Name = GetAttributeText(xmlNodeRace, "name");
            cls.Url = GetAttributeText(xmlNodeRace, "url");

            return cls;
        }

        private ClassFeature LoadClassFeature(XmlNode xmlNodeClassFeature)
        {
            ClassFeature classFeature = new ClassFeature();

            classFeature.Name = GetAttributeText(xmlNodeClassFeature, "name");
            string description = xmlNodeClassFeature.LastChild.Value;

            if (description == null)
            {
                description = GetDescendantNodeText(xmlNodeClassFeature, "specific[@name='Short Description']");
            }

            if (description != null)
            {
                // this is going to get embedded in some html later, so we do a bit of massaging
                classFeature.Description = description.Trim().Replace(Environment.NewLine, "<br />").Replace("<br /><br />", "<br />").Replace("<br />", "<br /><br />");
            }

            return classFeature;
        }

        private CharacterPower LoadPower(XmlNode xmlNodePower)
        {
            CharacterPower power = new CharacterPower();

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

        private Stat LoadStat(XmlNode xmlNodeStat)
        {
            Stat stat = new Stat();
            stat.Name = GetDescendantAttributeText(xmlNodeStat, "alias", "name").ToLower(); // seems to be some case issues between different files
            stat.Value = int.Parse(GetAttributeText(xmlNodeStat, "value"));

            return stat;
        }

        private Feat LoadFeat(XmlNode xmlNodeFeat)
        {
            Feat feat = new Feat();

            feat.Name = GetAttributeText(xmlNodeFeat, "name");
            feat.Url = GetAttributeText(xmlNodeFeat, "url");

            if (feat.Url != null)
            {
                feat.CompendiumEntry = CompendiumUtilities.GetFeat(feat.Url);
            }

            feat.Description = xmlNodeFeat.LastChild.Value.Trim();
            feat.ShortDescription = GetDescendantNodeText(xmlNodeFeat, "specific[@name='Short Description']");

            return feat;
        }

        private MagicItem LoadMagicItem(XmlNode xmlNodeMagicItem)
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

        private Weapon LoadWeapon(XmlNode xmlNodeWeapon)
        {
            Weapon weapon = new Weapon();

            weapon.Name = GetAttributeText(xmlNodeWeapon, "name");
            weapon.AttackBonus = GetWeaponAttackBonus(xmlNodeWeapon);
            weapon.Damage = GetWeaponDamage(xmlNodeWeapon);
            weapon.CriticalDamage = GetDescendantNodeText(xmlNodeWeapon, "CritDamage");
            weapon.AttackStat = GetDescendantNodeText(xmlNodeWeapon, "AttackStat");
            weapon.Defense = GetDescendantNodeText(xmlNodeWeapon, "Defense");

            return weapon;
        }

        private Power.UsageType GetPowerUsage(XmlNode xmlNodePower)
        {
            string usage = GetDescendantNodeText(xmlNodePower, "specific[@name='Power Usage']");

            return (usage != null) ? UsageFromStr(usage) : Power.UsageType.Undefined;
        }

        private CharacterPower.ActionType GetPowerAction(XmlNode xmlNodePower)
        {
            string action = GetDescendantNodeText(xmlNodePower, "specific[@name='Action Type']");

            return ActionFromStr(action);
        }

        private string GetPowerUrl(XmlNode xmlNodeUrls, string name)
        {
            // using \" here because some names have that darned apostrophe
            string url = GetDescendantAttributeText(xmlNodeUrls, string.Format("RulesElement[@name=\"{0}\" and @type=\"Power\"]", name), "url");

            return (url != null) ? url : null;
        }

        private string GetPowerAttackTypeAndRange(string entry)
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

        private bool GetPowerAllowsForMultipleAttacks(string entry)
        {
            Regex targetPattern = new Regex(@"<p[^>]*><b>\s*Targets?\s*</b>\s*:?\s*(.*?)</p>");
            Match match = targetPattern.Match(entry);

            bool allowsForMultipleAttacks = false;

            if (match.Success)
            {
                string targetInfo = match.Groups[1].Value;

                allowsForMultipleAttacks |= targetInfo.Contains("Each enemy");
                allowsForMultipleAttacks |= targetInfo.Contains("Each creature");
                allowsForMultipleAttacks |= targetInfo.Contains("One or two");
            }

            return allowsForMultipleAttacks;
        }

        private static int GetWeaponAttackBonus(XmlNode xmlNodeWeapon)
        {
            string attackBonus = GetDescendantNodeText(xmlNodeWeapon, "AttackBonus");

            return (attackBonus != null) ? int.Parse(attackBonus) : int.MinValue;
        }

        private string GetWeaponDamage(XmlNode xmlNodeWeapon)
        {
            string damage = GetDescendantNodeText(xmlNodeWeapon, "Damage");

            return string.IsNullOrEmpty(damage) ? "0" : damage;
        }

        private void GetMagicItemPower(MagicItem magicItem)
        {
            Regex usagePattern = new Regex(@"Power\s*\(([^)\s&]*)");
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
                magicItem.PowerAction = ActionFromStr(actionMatch.Groups[1].Value.Trim());
            }
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

            // Create a regular expression to match names that have invalid characters in them.
            // For example:
            //     <ID_INTERNAL_RITUAL_SCROLL_SCROLL_OF_CHAMELEON'S_CLOAK />
            Regex nameHasInvalidChars = new Regex(@"\x3C[\w']+");

            // Create a regular expression to match names that have invalid characters in them,
            // inside of parentheses.
            // For example:
            //     <ID_INTERNAL_CLASS_FEATURE_VESTIGE_PACT_(HYBRID) />
            //     <ID_INTERNAL_FEAT_FOCUSED_EXPERTISE_(TRIPLE-HEADED_FLAIL) />
            //     <ID_INTERNAL_FEAT_FOCUSED_EXPERTISE_(XEN'DRIK_BOOMERANG) />
            //     <ID_INTERNAL_CLASS_FEATURE_TRAVELER'S_UNPREDICTABLE_POWER_(FOR_CORMYR!) />
            //     <ID_INTERNAL_CLASS_FEATURE_TRAVELER'S_UNPREDICTABLE_POWER_(SILVER_TONGUE,_SILVER_BLADE) />
            Regex nameHasInvalidCharsWithParens = new Regex(@"\x3C[\w']+\x28[\w'\!\-\,]+\x29");

            // Copy the file
            string text;
            do
            {
                text = input.ReadLine();

                // Find names that have parentheses in them and remove the parentheses.
                if (text != null && (nameHasInvalidChars.IsMatch(text) || nameHasInvalidCharsWithParens.IsMatch(text)))
                {
                    text = text.Replace("(", "");
                    text = text.Replace(")", "");
                    text = text.Replace("'", "-");
                    text = text.Replace("!", "-");
                    text = text.Replace(",", "-");
                }

                output.WriteLine(text);
            } while (text != null);
            input.Close();
            output.Close();

            return tempFilename;
        }

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

        public Character Character
        {
            get { return mCharacter; }
        }
        private Character mCharacter = new Character();
    }
}
