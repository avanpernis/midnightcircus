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

            XmlNodeList xmlNodeWeapons = xmlNodePower.SelectNodes("Weapon");

            foreach (XmlNode xmlNodeWeapon in xmlNodeWeapons)
            {
                Weapon weapon = LoadWeapon(xmlNodeWeapon);

                power.Weapons.Add(weapon);
            }

            return power;
        }

        private static Weapon LoadWeapon(XmlNode xmlNodeWeapon)
        {
            Weapon weapon = new Weapon();

            weapon.Name = GetAttributeText(xmlNodeWeapon, "name");
            weapon.AttackBonus = GetWeaponAttackBonus(xmlNodeWeapon);
            weapon.Damage = GetWeaponDamage(xmlNodeWeapon);

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
