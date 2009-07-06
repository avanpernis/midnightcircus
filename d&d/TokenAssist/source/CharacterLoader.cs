using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            XmlNode xmlNodePowersRoot = xmlDocument.SelectSingleNode("//PowerStats");

            // we use this to get the url which contains extra information about each power
            XmlNode xmlNodeUrlsRoot = xmlDocument.SelectSingleNode("//RulesElementTally");

            foreach (XmlNode xmlNodePower in xmlNodePowersRoot.ChildNodes)
            {
                Power power = LoadPower(xmlNodePower);
                power.Url = GetPowerUrl(xmlNodeUrlsRoot, power.Name);

                if (power.Url != null)
                {
                    power.CompendiumEntry = CompendiumUtilities.GetEntry(power.Url);
                }

                character.Powers.Add(power);
            }

            return character;
        }

        private static Power LoadPower(XmlNode xmlNodePower)
        {
            Power power = new Power();

            power.Name = GetAttributeText(xmlNodePower, "name");
            power.Usage = GetPowerUsage(xmlNodePower);
            power.Action = GetPowerAction(xmlNodePower);
            power.AttackBonus = GetPowerAttackBonus(xmlNodePower);
            power.Damage = GetPowerDamage(xmlNodePower);

            return power;
        }

        private static Power.UsageType GetPowerUsage(XmlNode xmlNodePower)
        {
            string usage = GetDescendantNodeText(xmlNodePower, "specific[@name='Power Usage']");

            switch (usage)
            {
                case "At-Will":
                    return Power.UsageType.AtWill;
                case "Encounter":
                    return Power.UsageType.Encounter;
                case "Daily":
                    return Power.UsageType.Daily;
                default:
                    return Power.UsageType.Undefined;
            }
        }

        private static Power.ActionType GetPowerAction(XmlNode xmlNodePower)
        {
            string action = GetDescendantNodeText(xmlNodePower, "specific[@name='Action Type']");

            switch (action)
            {
                case "Free":
                    return Power.ActionType.Free;
                case "Minor":
                    return Power.ActionType.Minor;
                case "Move":
                    return Power.ActionType.Move;
                case "Standard":
                    return Power.ActionType.Standard;
                case "Immediate Interrupt":
                    return Power.ActionType.ImmediateInterrupt;
                case "Immediate Reaction":
                    return Power.ActionType.ImmediateReaction;
                default:
                    return Power.ActionType.Undefined;
            }
        }

        private static int GetPowerAttackBonus(XmlNode xmlNodePower)
        {
            string attackBonus = GetDescendantNodeText(xmlNodePower, "Weapon/AttackBonus");

            return (attackBonus != null) ? int.Parse(attackBonus) : Power.DefaultAttackBonus;
        }

        private static string GetPowerDamage(XmlNode xmlNodePower)
        {
            string damage = GetDescendantNodeText(xmlNodePower, "Weapon/Damage");

            return (damage != null) ? damage : Power.DefaultDamage;
        }

        private static string GetPowerUrl(XmlNode xmlNodeUrls, string name)
        {
            // using \" here because some names have that darned apostrophe
            string url = GetDescendantAttributeText(xmlNodeUrls, string.Format("RulesElement[@name=\"{0}\"]", name), "url");

            return (url != null) ? url : null;
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

//        <Power name="Melee Basic Attack">
//            <specific name="Power Usage"> At-Will </specific>
//            <specific name="Action Type"> Standard Action </specific>
//            <Weapon name="Pinning Battleaxe +2">
//               <RulesElement name="Battleaxe" type="Weapon" internal-id="ID_FMP_WEAPON_12" url="http://www.wizards.com/dndinsider/compendium/item.aspx?fid=12&amp;ftype=3" charelem="b32d870" legality="rules-legal" />
//               <RulesElement name="Pinning Weapon +2" type="Magic Item" internal-id="ID_FMP_MAGIC_ITEM_2261" url="http://www.wizards.com/dndinsider/compendium/item.aspx?fid=2261&amp;ftype=1" charelem="b302638" legality="rules-legal" />
//               <AttackBonus> 13 </AttackBonus>
//               <Damage> 1d10+8 </Damage>
//               <AttackStat> Strength </AttackStat>
//               <Defense> AC </Defense>
//               <HitComponents> +5 Strength modifier.
//+3 half your level.
//+2 proficiency bonus.
//+2 enhancement bonus.
//+1 bonus - Weapon Expertise (Axe)

// </HitComponents>
//               <DamageComponents> +5 Strength modifier.
//+2 enhancement bonus.
//+1 Feat bonus - Weapon Focus (Axe)

// </DamageComponents>
//            </Weapon>
//         </Power>
    }
}
