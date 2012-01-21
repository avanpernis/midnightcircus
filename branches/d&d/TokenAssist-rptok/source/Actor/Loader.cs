using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TokenAssist
{
    public class Loader
    {
        /// <summary>
        ///  Convert from string representing usage type to the enumeration
        /// </summary>
        /// <param name="usageStr"></param>
        /// <returns></returns>
        public static Power.UsageType UsageFromStr(string usageStr)
        {
            usageStr = usageStr.Replace("-", ""); // remove the hyphen so that we can enum parse for 'at-will'
            usageStr = usageStr.Replace(" Special", ""); // remove 'Special' so that we can enum parse for 'Encounter Special'

            return (Power.UsageType)Enum.Parse(typeof(Power.UsageType), usageStr);
        }

        public static Power.ActionType ActionFromStr(string actionStr)
        {
            actionStr = actionStr.Replace("Action", ""); // remove "Action" suffix so that we can enum parse for 'free', 'minor', 'move', and 'standard
            actionStr = actionStr.Replace(" action", ""); // sometimes there is a lower case 'a'
            actionStr = actionStr.Replace("(Special)", ""); // some actions have a special qualifier, but we don't care about that for purposes of token creation
            actionStr = actionStr.Replace("Special", "");
            actionStr = actionStr.Replace("</b>", ""); //remove bold formatting from some actions
            actionStr = actionStr.Replace(" ", ""); // remove spaces so that we can enum parse for 'immediate interrupt' and 'immediate reaction'

            return (CharacterPower.ActionType)Enum.Parse(typeof(CharacterPower.ActionType), actionStr);
        }

        protected static string GetDescendantAttributeText(XmlNode xmlNodeParent, string xPath, string attributeName)
        {
            XmlNode xmlNodeDescendant = xmlNodeParent.SelectSingleNode(xPath);

            return (xmlNodeDescendant != null) ? GetAttributeText(xmlNodeDescendant, attributeName) : null;
        }

        protected static string GetAttributeText(XmlNode xmlNode, string attributeName)
        {
            XmlAttribute xmlAttribute = xmlNode.Attributes[attributeName];

            return (xmlAttribute != null) ? xmlAttribute.InnerText.Trim() : null;
        }

        protected static string GetDescendantNodeText(XmlNode xmlNodeParent, string xPath)
        {
            XmlNode xmlNodeDescendant = xmlNodeParent.SelectSingleNode(xPath);

            return (xmlNodeDescendant != null) ? xmlNodeDescendant.InnerText.Trim() : null;
        }
    }
}
